using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class frm_Client : Form
    {
        private const string CLOSE_COMMAND = "0";
        private const string SEND_MESSAGE = "1";
        private const string SEND_IMAGE = "2";
        private const string SEND_FILE = "3";
        private const string SEND_EMOJI = "4";

        private Socket client;
        private IPEndPoint iPEndPoint;
        private const int PORT = 5555;
        private byte[] buffer;
        private Image prevEmoji = null;
        public frm_Client()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT);
        }

        private void frm_Client_Load(object sender, EventArgs e)
        {
            startClient();
            pnlMain.Controls.Add(createChatPanelView());
            loadEmoji();
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


                client.Send(mergedArray);

                Label labelTime = createChatTime();
                Panel panelEmoji = createChatEmoji(prevEmoji, DockStyle.Right);
                panelEmoji.Tag = labelTime;

                pnlMain.Controls[0].Controls.Add(labelTime);
                labelTime.BringToFront();
                pnlMain.Controls[0].Controls.Add(panelEmoji);
                panelEmoji.BringToFront();
                (pnlMain.Controls[0] as Panel).ScrollControlIntoView(panelEmoji);

                ms.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi gửi hình ảnh");
            }
        }

        private void startClient()
        {
            client.BeginConnect(iPEndPoint, connectCallback, null);
        }

        private void connectCallback(IAsyncResult ar)
        {
            try
            {
                client.EndConnect(ar);
                BeginInvoke((Action)(() =>
                {
                    CheckBox checkBox = createChatClient(iPEndPoint.ToString() + " (Server)");
                    checkBox.Checked = true;
                    pnlSidebar.Controls.Add(checkBox);

                    lblUsername.Text = checkBox.Text;

                    buffer = new byte[1024 * 20];
                    client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, receiveCallback, null);
                }));
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi kết nối đến máy chủ");
            }
        }

        private void receiveCallback(IAsyncResult ar)
        {
            try
            {
                int bytesRead = client.EndReceive(ar);
                if (bytesRead > 0)
                {
                    byte[] receivedData = new byte[bytesRead];
                    Array.Copy(buffer, receivedData, bytesRead);

                    string dataType = Encoding.UTF8.GetString(receivedData, 0, 1);

                    switch (dataType)
                    {
                        case SEND_MESSAGE:
                            string message = Encoding.UTF8.GetString(receivedData, 1, bytesRead - 1);
                            BeginInvoke((Action)(() =>
                            {
                                Label labelTime = createChatTime();
                                Panel panelMessage = createChatMessage(message, DockStyle.Left);
                                panelMessage.Tag = labelTime;

                                pnlMain.Controls[0].Controls.Add(labelTime);
                                labelTime.BringToFront();
                                pnlMain.Controls[0].Controls.Add(panelMessage);
                                panelMessage.BringToFront();
                                (pnlMain.Controls[0] as Panel).ScrollControlIntoView(panelMessage);

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

                                pnlMain.Controls[0].Controls.Add(labelTime);
                                labelTime.BringToFront();
                                pnlMain.Controls[0].Controls.Add(panelImage);
                                panelImage.BringToFront();
                                (pnlMain.Controls[0] as Panel).ScrollControlIntoView(panelImage);

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

                                pnlMain.Controls[0].Controls.Add(labelTime);
                                labelTime.BringToFront();
                                pnlMain.Controls[0].Controls.Add(panelEmoji);
                                panelEmoji.BringToFront();
                                (pnlMain.Controls[0] as Panel).ScrollControlIntoView(panelEmoji);

                            }));
                            break;
                    }

                    buffer = new byte[1024 * 20];
                    client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, receiveCallback, null);
                }
                else
                {
                    client.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi nhận dữ liệu từ máy chủ");
            }
        }

        private void sendMessage(string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                client.BeginSend(data, 0, data.Length, SocketFlags.None, sendCallback, null);
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi gửi dữ liệu đến máy chủ");
            }
        }

        private void sendCallback(IAsyncResult ar)
        {
            try
            {
                client.EndSend(ar);
                BeginInvoke((Action)(() =>
                {

                    Label labelTime = createChatTime();
                    Panel panelMessage = createChatMessage(txtMessage.Text, DockStyle.Right);
                    panelMessage.Tag = labelTime;

                    pnlMain.Controls[0].Controls.Add(labelTime);
                    labelTime.BringToFront();
                    pnlMain.Controls[0].Controls.Add(panelMessage);
                    panelMessage.BringToFront();
                    (pnlMain.Controls[0] as Panel).ScrollControlIntoView(panelMessage);

                    txtMessage.Clear();
                    txtMessage.Focus();
                }));
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi kết thúc gửi dữ liệu máy chủ");
            }
        }

        private string getCurrentDateTime()
        {
            return DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
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

        private Panel createChatPanelView()
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.ForeColor = Color.Black;
            panel.Padding = new Padding(20, 10, 20, 20);
            panel.BackColor = Color.White;
            panel.AutoScroll = true;
            return panel;
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
            /*checkBox.BackColor = clients.Count == 1 ? Color.FromArgb(229, 239, 255) : Color.White;
            checkBox.Checked = clients.Count == 1;*/
            checkBox.CheckedChanged += chatClient_CheckedChanged;
            checkBox.Click += chatClient_Click;

            return checkBox;
        }

        private void chatClient_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chatLabel = sender as CheckBox;

            if (chatLabel.Checked)
            {
                chatLabel.BackColor = Color.FromArgb(229, 239, 255);
            }
            else
            {
                chatLabel.BackColor = Color.White;
            }

        }

        private void chatClient_Click(object sender, EventArgs e)
        {
            CheckBox chatLabel = sender as CheckBox;

            lblUsername.Text = chatLabel.Text;

            int index = pnlSidebar.Controls.GetChildIndex(chatLabel);
            /*panelViews[currentClientIndex].Visible = false;
            currentClientIndex = index;
            panelViews[currentClientIndex].Visible = true;*/
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

        private void ptbDelete_Click(object sender, EventArgs e)
        {
            try
            {
                PictureBox ptbDelete = sender as PictureBox;
                Control topParent = ptbDelete.Parent.Parent;
                Control time = (Control)topParent.Tag;
                pnlMain.Controls[0].Controls.Remove(topParent);
                pnlMain.Controls[0].Controls.Remove(time);
            }
            catch (Exception)
            {

            }
        }

        public Size calculateAspectRatio(Size originalSize, int targetWidth)
        {
            int targetHeight = (targetWidth * originalSize.Height) / originalSize.Width;

            return new Size(targetWidth, targetHeight);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập nội dung tin nhắn");
                return;
            }

            sendMessage(SEND_MESSAGE + txtMessage.Text);

        }

        private void frm_Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng ứng dụng?", "Xác nhận đóng ứng dụng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    sendMessage(CLOSE_COMMAND);
                    client.Close();
                    e.Cancel = false;
                }
            }
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

        private void lblHint_Click(object sender, EventArgs e)
        {
            lblHint.Visible = false;
            txtMessage.Visible = true;
            txtMessage.Focus();
        }

        private void ptbImage_Click(object sender, EventArgs e)
        {
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

                    client.Send(mergedArray);

                    ImageConverter convertData = new ImageConverter();
                    Image image = (Image)convertData.ConvertFrom(imageData);

                    Label labelTime = createChatTime();
                    Panel panelImage = createChatImage(image, DockStyle.Right);
                    panelImage.Tag = labelTime;

                    pnlMain.Controls[0].Controls.Add(labelTime);
                    labelTime.BringToFront();
                    pnlMain.Controls[0].Controls.Add(panelImage);
                    panelImage.BringToFront();
                    (pnlMain.Controls[0] as Panel).ScrollControlIntoView(panelImage);

                    bmp.Dispose();
                    ms.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi gửi hình ảnh");
                }
            }
        }

        private void frm_Client_SizeChanged(object sender, EventArgs e)
        {
            pnlLeft.Visible = Width > 800;
        }

        private void ptbEmoji_Click(object sender, EventArgs e)
        {
            pnlEmoji.Visible = !pnlEmoji.Visible;
        }

        private void ptbFile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng đang phát triển");
        }
    }
}
