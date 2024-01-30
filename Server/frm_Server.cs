using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    public partial class frm_Server : Form
    {
        private const string CLOSE_COMMAND = "0";
        private const string SEND_MESSAGE = "1";
        private const string SEND_IMAGE = "2";
        private const string SEND_FILE = "3";
        private const string SEND_EMOJI = "4";

        private List<Socket> clients;
        private List<Panel> panelViews;
        private List<byte[]> buffers;
        private Socket server;
        private bool listening = false;
        private const int PORT = 5555;
        private delegate void SocketAcceptedHandler(Socket socket);
        private event SocketAcceptedHandler socketAccepted;
        private int currentClientIndex = 0;
        private string prevMessage = "";
        private Image prevEmoji = null;
        private bool hasClient = false;
        public frm_Server()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;
            clients = new List<Socket>();
            panelViews = new List<Panel>();
            buffers = new List<byte[]>();
            socketAccepted += new SocketAcceptedHandler(socketAcceptedCallback);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void socketAcceptedCallback(Socket socket)
        {
            clients.Add(socket);
            BeginInvoke((Action)(() =>
            {
                if (!hasClient) pnlMain.Controls.Clear();

                CheckBox checkBox = createChatClient(socket.RemoteEndPoint.ToString());
                Panel panel = createChatPanelViewItem();

                hasClient = true;

                if (clients.Count == 1)
                {
                    lblUsername.Text = checkBox.Text;
                }

                pnlSidebar.Controls.Add(checkBox);
                panelViews.Add(panel);

                pnlMain.Controls.Add(panel);

                byte[] buffer = new byte[1024 * 20];
                buffers.Add(buffer);
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, receiveCallback, socket);
            }));
        }

        private void receiveCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            try
            {
                int clientIndex = getClientIndex(socket);

                int bytesRead = socket.EndReceive(ar);

                if (bytesRead > 0)
                {
                    byte[] receivedData = new byte[bytesRead];
                    Array.Copy(buffers[clientIndex], receivedData, bytesRead);

                    string dataType = Encoding.UTF8.GetString(receivedData, 0, 1);

                    switch (dataType)
                    {
                        case CLOSE_COMMAND:
                            handleClientDisconnect(socket);
                            return;

                        case SEND_MESSAGE:
                            string message = Encoding.UTF8.GetString(receivedData, 1, bytesRead - 1);
                            BeginInvoke((Action)(() =>
                            {
                                Label labelTime = createChatTime();
                                Panel panelMessage = createChatMessage(message, DockStyle.Left);
                                panelMessage.Tag = labelTime;

                                panelViews[clientIndex].Controls.Add(labelTime);
                                labelTime.BringToFront();
                                panelViews[clientIndex].Controls.Add(panelMessage);
                                panelMessage.BringToFront();
                                panelViews[clientIndex].ScrollControlIntoView(panelMessage);

                            }));
                            break;
                        case SEND_IMAGE:
                            BeginInvoke((Action)(() =>
                            {
                                ImageConverter convertData = new ImageConverter();
                                Image image = (Image)convertData.ConvertFrom(receivedData.Skip(1).ToArray());

                                Label labelTime = createChatTime();
                                Panel panelImage = createChatImage(image, DockStyle.Left);
                                panelImage.Tag = labelTime;

                                panelViews[clientIndex].Controls.Add(labelTime);
                                labelTime.BringToFront();
                                panelViews[clientIndex].Controls.Add(panelImage);
                                panelImage.BringToFront();
                                panelViews[clientIndex].ScrollControlIntoView(panelImage);

                            }));
                            break;
                        case SEND_FILE:

                            break;
                        case SEND_EMOJI:
                            BeginInvoke((Action)(() =>
                            {
                                ImageConverter convertData = new ImageConverter();
                                Image image = (Image)convertData.ConvertFrom(receivedData.Skip(1).ToArray());

                                Label labelTime = createChatTime();
                                Panel panelEmoji = createChatEmoji(image, DockStyle.Left);
                                panelEmoji.Tag = labelTime;

                                panelViews[clientIndex].Controls.Add(labelTime);
                                labelTime.BringToFront();
                                panelViews[clientIndex].Controls.Add(panelEmoji);
                                panelEmoji.BringToFront();
                                panelViews[clientIndex].ScrollControlIntoView(panelEmoji);

                            }));
                            break;

                    }
                    buffers[clientIndex] = new byte[1024 * 20];
                    socket.BeginReceive(buffers[clientIndex], 0, buffers[clientIndex].Length, SocketFlags.None, receiveCallback, socket);
                }
            }
            catch (Exception)
            {
                handleClientDisconnect(socket);
            }
        }

        private void handleClientDisconnect(Socket client)
        {
            int index = getClientIndex(client);
            if (index != -1)
            {
                BeginInvoke((Action)(() =>
                {
                    pnlSidebar.Controls[currentClientIndex].BackColor = Color.White;
                    pnlSidebar.Controls.RemoveAt(index);
                    pnlMain.Controls.RemoveAt(index);
                    panelViews.RemoveAt(index);
                    clients.RemoveAt(index);
                    buffers.RemoveAt(index);
                    setCurrentClient();
                    client.Close();
                }));
            }
        }

        private void frm_Server_Load(object sender, EventArgs e)
        {
            start();
            loadEmoji();
        }

        private void start()
        {
            if (listening) return;
            server.Bind(new IPEndPoint(IPAddress.Any, PORT));
            server.Listen(10);
            server.BeginAccept(beginAcceptCallback, null);

            listening = true;
        }

        private void stop()
        {
            if (!listening) return;
            server.Close();
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            listening = false;
        }

        private void beginAcceptCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = server.EndAccept(ar);

                if (socketAccepted != null)
                {
                    socketAccepted(client);
                }

                server.BeginAccept(beginAcceptCallback, null);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Panel createChatPanelViewItem()
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.ForeColor = Color.Black;
            panel.Padding = new Padding(20, 10, 20, 20);
            panel.BackColor = Color.White;
            panel.AutoScroll = true;
            panel.Visible = currentClientIndex == 0 ? true : false;
            return panel;
        }

        private Panel createChatMessage(string message, DockStyle dock)
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;

            Label label = new Label();
            label.Text = message;
            label.Dock = dock;
            label.MaximumSize = new Size(300, 500);
            label.AutoSize = true;
            label.Font = new Font(label.Font.FontFamily, 12);
            label.ForeColor = dock == DockStyle.Right ? Color.White : Color.Black;
            label.Padding = new Padding(10);
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.BackColor = dock == DockStyle.Right ? Color.FromArgb(0, 145, 255) : Color.FromArgb(229, 239, 255);
            label.AutoEllipsis = true;

            PictureBox ptbDelete = new PictureBox();
            ptbDelete.Size = new Size(18, 18);
            ptbDelete.Image = Properties.Resources.ic_delete;
            ptbDelete.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbDelete.Dock = DockStyle.Bottom;

            Panel pnlDelete = new Panel();
            pnlDelete.Dock = dock;
            pnlDelete.Controls.Add(ptbDelete);
            pnlDelete.Size = new Size(28, pnlDelete.Height);
            pnlDelete.Padding = new Padding(5, 5, 5, 10);

            panel.Controls.Add(label);
            panel.Height = label.Height;
            panel.Controls.Add(pnlDelete);
            pnlDelete.BringToFront();

            ptbDelete.Click += ptbDelete_Click;

            return panel;
        }

        private void ptbDelete_Click(object sender, EventArgs e)
        {
            try
            {
                PictureBox ptbDelete = sender as PictureBox;
                Control topParent = ptbDelete.Parent.Parent;
                Control time = (Control)topParent.Tag;
                panelViews[currentClientIndex].Controls.Remove(topParent);
                panelViews[currentClientIndex].Controls.Remove(time);
            }
            catch (Exception)
            {

            }
        }

        private Panel createChatEmoji(Image image, DockStyle dock)
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;

            PictureBox picture = new PictureBox();
            picture.AutoSize = false;
            picture.Dock = dock;
            picture.Size = new Size(50, 50);
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.Image = image;
            picture.BackColor = dock == DockStyle.Right ? Color.FromArgb(0, 145, 255) : Color.FromArgb(229, 239, 255);
            picture.Padding = new Padding(10);

            PictureBox ptbDelete = new PictureBox();
            ptbDelete.Size = new Size(18, 18);
            ptbDelete.Image = Properties.Resources.ic_delete;
            ptbDelete.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbDelete.Dock = DockStyle.Bottom;

            Panel pnlDelete = new Panel();
            pnlDelete.Dock = dock;
            pnlDelete.Controls.Add(ptbDelete);
            pnlDelete.Size = new Size(28, pnlDelete.Height);
            pnlDelete.Padding = new Padding(5, 5, 5, 10);

            panel.Controls.Add(picture);
            panel.Size = new Size(50, 50);
            panel.Controls.Add(pnlDelete);
            pnlDelete.BringToFront();

            ptbDelete.Click += ptbDelete_Click;

            return panel;
        }

        private Panel createChatImage(Image image, DockStyle dock)
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;

            PictureBox picture = new PictureBox();
            picture.AutoSize = false;
            picture.Dock = dock;
            picture.Size = calculateAspectRatio(image.Size, 300);
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.Image = image;
            picture.MaximumSize = new Size(300, 700);
            picture.BackColor = Color.White;

            PictureBox ptbDelete = new PictureBox();
            ptbDelete.Size = new Size(18, 18);
            ptbDelete.Image = Properties.Resources.ic_delete;
            ptbDelete.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbDelete.Dock = DockStyle.Bottom;

            Panel pnlDelete = new Panel();
            pnlDelete.Dock = dock;
            pnlDelete.Controls.Add(ptbDelete);
            pnlDelete.Size = new Size(28, pnlDelete.Height);
            pnlDelete.Padding = new Padding(5, 5, 5, 10);

            panel.Controls.Add(picture);
            panel.Size = calculateAspectRatio(image.Size, 300);
            panel.Controls.Add(pnlDelete);
            pnlDelete.BringToFront();

            ptbDelete.Click += ptbDelete_Click;

            return panel;
        }

        public Size calculateAspectRatio(Size originalSize, int targetWidth)
        {
            int targetHeight = (targetWidth * originalSize.Height) / originalSize.Width;

            return new Size(targetWidth, targetHeight);
        }

        private Label createChatTime()
        {
            Label label = new Label();
            label.Text = getCurrentDateTime();
            label.AutoSize = false;
            label.Dock = DockStyle.Top;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font(label.Font.FontFamily, 9);
            label.Size = new Size(0, 45);
            label.ForeColor = Color.FromArgb(140, 140, 140);

            return label;
        }

        private CheckBox createChatClient(string chatName)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Text = chatName;
            checkBox.AutoSize = false;
            checkBox.Dock = DockStyle.Top;
            checkBox.Size = new Size(0, 50);
            checkBox.ForeColor = Color.Black;
            checkBox.Padding = new Padding(10);
            checkBox.TextAlign = ContentAlignment.MiddleLeft;
            checkBox.BackColor = clients.Count == 1 ? Color.FromArgb(229, 239, 255) : Color.White;
            checkBox.Checked = clients.Count == 1;
            checkBox.Click += chatClient_Click;
            return checkBox;
        }

        private void chatClient_Click(object sender, EventArgs e)
        {
            CheckBox chatLabel = sender as CheckBox;

            int index = pnlSidebar.Controls.GetChildIndex(chatLabel);

            if (index == currentClientIndex) return;

            pnlSidebar.Controls[currentClientIndex].BackColor = Color.White;

            chatLabel.BackColor = Color.FromArgb(229, 239, 255);

            lblUsername.Text = chatLabel.Text;


            panelViews[currentClientIndex].Visible = false;
            currentClientIndex = index;
            panelViews[currentClientIndex].Visible = true;
        }

        private void setCurrentClient()
        {
            if (clients.Count == 0)
            {
                hasClient = false;
                currentClientIndex = 0;
                lblUsername.Text = "";
                pnlMain.Controls.Add(createEmptyLabel());
            }
            else
            {
                currentClientIndex = pnlSidebar.Controls.Count - 1;
                panelViews[currentClientIndex].Visible = true;
                (pnlSidebar.Controls[currentClientIndex] as CheckBox).Checked = true;
                pnlSidebar.Controls[currentClientIndex].BackColor = Color.FromArgb(229, 239, 255);
            }
        }

        private Label createEmptyLabel()
        {
            Label label = new Label();
            label.Text = "Không có người dùng nào để nhắn tin";
            label.AutoSize = false;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font(label.Font.FontFamily, 12);
            return label;
        }

        private int getClientIndex(Socket socket)
        {
            return clients.IndexOf(socket);
        }

        private void frm_Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            stop();
        }

        private string getCurrentDateTime()
        {
            return DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            if (txtMessage.Text.Length > 0)
            {
                lblHint.Visible = false;
            }
            else
            {
                lblHint.Visible = true;
            }
        }

        private void loadEmoji()
        {
            string[] files = Directory.GetFiles("Emojis");
            foreach (string file in files)
            {
                PictureBox picture = new PictureBox();
                picture.Size = new Size(28, 28);
                picture.Image = Image.FromFile(file);
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                picture.Padding = new Padding(2);
                pnlEmoji.Controls.Add(picture);

                picture.Click += ptbEmojiItem_Click;
            }
        }

        private void ptbEmojiItem_Click(object sender, EventArgs e)
        {
            prevEmoji = (sender as PictureBox).Image;
            pnlEmoji.Visible = false;
            try
            {
                byte[] dataType = Encoding.UTF8.GetBytes(SEND_EMOJI);

                MemoryStream ms = new MemoryStream();
                prevEmoji.Save(ms, ImageFormat.Png);

                byte[] imageData = ms.ToArray();

                byte[] mergedArray = dataType.Concat(imageData).ToArray();

                int numberOfRecipients = 0;
                for (int i = 0; i < pnlSidebar.Controls.Count; i++)
                {
                    if ((pnlSidebar.Controls[i] as CheckBox).Checked)
                    {
                        clients[i].Send(mergedArray);

                        Label labelTime = createChatTime();
                        Panel panelEmoji = createChatEmoji(prevEmoji, DockStyle.Right);
                        panelEmoji.Tag = labelTime;

                        panelViews[i].Controls.Add(labelTime);
                        labelTime.BringToFront();
                        panelViews[i].Controls.Add(panelEmoji);
                        panelEmoji.BringToFront();
                        panelViews[i].ScrollControlIntoView(panelEmoji);
                        numberOfRecipients++;
                    }
                }

                if (numberOfRecipients == 0)
                {
                    MessageBox.Show("Vui lòng chọn người để gửi ảnh");
                }

                ms.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi gửi hình ảnh");
            }
        }

        private void lblHint_Click(object sender, EventArgs e)
        {
            lblHint.Visible = false;
            txtMessage.Visible = true;
            txtMessage.Focus();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (clients.Count == 0)
            {
                MessageBox.Show("Chưa có người dùng để chat");
                return;
            }

            if (txtMessage.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập nội dung tin nhắn");
                return;
            }

            int numberOfRecipients = 0;
            prevMessage = txtMessage.Text;

            for (int i = 0; i < pnlSidebar.Controls.Count; i++)
            {
                if ((pnlSidebar.Controls[i] as CheckBox).Checked)
                {
                    sendMessage(SEND_MESSAGE + prevMessage, i);
                    numberOfRecipients++;
                }
            }

            if (numberOfRecipients == 0)
            {
                MessageBox.Show("Vui lòng chọn người gửi tin nhắn");
            }

        }

        private void sendMessage(string message, int index)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                clients[index].BeginSend(data, 0, data.Length, SocketFlags.None, sendCallback, clients[index]);
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi gửi dữ liệu đến máy khách");
            }
        }

        private void sendCallback(IAsyncResult ar)
        {
            try
            {
                Socket socket = (Socket)ar.AsyncState;
                socket.EndSend(ar);
                int clientIndex = getClientIndex(socket);

                BeginInvoke((Action)(() =>
                {
                    Label labelTime = createChatTime();
                    Panel panelMessage = createChatMessage(prevMessage, DockStyle.Right);
                    panelMessage.Tag = labelTime;

                    panelViews[clientIndex].Controls.Add(labelTime);
                    labelTime.BringToFront();
                    panelViews[clientIndex].Controls.Add(panelMessage);
                    panelMessage.BringToFront();
                    panelViews[clientIndex].ScrollControlIntoView(panelMessage);

                    txtMessage.Clear();
                    txtMessage.Focus();
                }));
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi kết thúc gửi dữ liệu");
            }
        }

        private void ptbImage_Click(object sender, EventArgs e)
        {
            if (clients.Count == 0)
            {
                MessageBox.Show("Chưa có người dùng để chat");
                return;
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    byte[] dataType = Encoding.UTF8.GetBytes(SEND_IMAGE);

                    Bitmap bmp = new Bitmap(openFileDialog.FileName);
                    MemoryStream ms = new MemoryStream();

                    ImageFormat format = openFileDialog.FileName.EndsWith(".jpg")
                        || openFileDialog.FileName.EndsWith(".jpeg") ?
                        ImageFormat.Jpeg : ImageFormat.Png;

                    bmp.Save(ms, format);
                    byte[] imageData = ms.ToArray();

                    byte[] mergedArray = dataType.Concat(imageData).ToArray();

                    int numberOfRecipients = 0;
                    for (int i = 0; i < pnlSidebar.Controls.Count; i++)
                    {
                        if ((pnlSidebar.Controls[i] as CheckBox).Checked)
                        {
                            clients[i].Send(mergedArray);
                            ImageConverter convertData = new ImageConverter();
                            Image image = (Image)convertData.ConvertFrom(imageData);

                            Label labelTime = createChatTime();
                            Panel panelImage = createChatImage(image, DockStyle.Right);
                            panelImage.Tag = labelTime;

                            panelViews[i].Controls.Add(labelTime);
                            labelTime.BringToFront();
                            panelViews[i].Controls.Add(panelImage);
                            panelImage.BringToFront();
                            panelViews[i].ScrollControlIntoView(panelImage);
                            numberOfRecipients++;
                        }
                    }

                    if (numberOfRecipients == 0)
                    {
                        MessageBox.Show("Vui lòng chọn người để gửi ảnh");
                    }

                    bmp.Dispose();
                    ms.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi gửi hình ảnh");
                }
            }
        }

        private void frm_Server_SizeChanged(object sender, EventArgs e)
        {
            pnlLeft.Visible = Width > 800;
        }

        private void ptbEmoji_Click(object sender, EventArgs e)
        {
            if (clients.Count == 0)
            {
                MessageBox.Show("Chưa có người dùng để chat");
                return;
            }
            pnlEmoji.Visible = !pnlEmoji.Visible;
        }

        private void ptbFile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng đang phát triển");
        }
    }
}
