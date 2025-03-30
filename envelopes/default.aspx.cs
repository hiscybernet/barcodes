using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Text.RegularExpressions;

public partial class _default : System.Web.UI.Page
{
    private Bitmap pic = new Bitmap(950, 412, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.PD = new System.Drawing.Printing.PrintDocument();
            //this.PD.PrintPage += new PrintPageEventHandler(this.PD_PrintPage);
            //foreach (PaperSize paperSiz in this.PD.PrinterSettings.PaperSizes)
            //{
            //    if (paperSiz.PaperName == "Envelope #10")
            //        this.PD.DefaultPageSettings.PaperSize = paperSiz;
            //}
            //this.PD.DefaultPageSettings.Landscape = true;

            //var confirmResult = MessageBox.Show("Philip's Inteligent Mail Barcode will transition to a\r\n100% cloud based subscription service on September 1, 2020.\r\nThe Microsoft Windows desktop version will cease to function.\r\nThis change was needed to have one app that will support all internet connected devices such as tablets, phones, and desktop computers.\r\nA modern web browser that can print PDF files is required.",
            //"Philip's Inteligent Mail Barcode", MessageBoxButtons.OK);
        }
    }
    private string CalculateOneCode()
    {
        string str1;
        if (this.TBADDRESSEE.Rows >= 3)
        {
            string str2 = Regex.Match(TBADDRESSEE.Text, "\\d\\d\\d\\d\\d").ToString();
            if (str2 != "")
            {
                str1 = OneCode.Bars("00702000000000000000" + str2);
                ///str1 = OneCode.Bars("00702000000000000000" + "78412");
                //this.BTNPRINT.Enabled = true;
            }
            else
            {
                str1 = "";
                //this.BTNPRINT.Enabled = false;
            }
        }
        else
        {
            str1 = "";
            //this.BTNPRINT.Enabled = false;
        }
        return str1;
    }
    public byte[] ImageToByteArray(System.Drawing.Image imageIn)
    {
        using (var ms = new System.IO.MemoryStream())
        {
            imageIn.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
    private void UpdatePic()
    {
        Graphics graphics = Graphics.FromImage((System.Drawing.Image)this.pic);
        graphics.Clear(Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue));
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        graphics.DrawString(this.TBRETURNADDRESS.Text, new System.Drawing.Font(this.TBRETURNADDRESS.Font.Name, 12f), Brushes.Black, new PointF(10f, 10f));
        graphics.DrawString(this.CalculateOneCode(), new Font("USPSIMBStandard", 16f), Brushes.Black, 350f, 200f);
        graphics.DrawString(this.TBADDRESSEE.Text, new Font(this.TBADDRESSEE.Font.Name, 14.25f), Brushes.Black, new PointF(350f, 222f));
        //PREVIEW.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String(ImageToByteArray((System.Drawing.Image)pic));
        //PREVIEW.BorderStyle = System.Web.UI.WebControls.BorderStyle.Outset;
        //PREVIEW.Width = 300;
    }
    protected void BTNPRINT_Click(object sender, EventArgs e)
    {
        UpdatePic();
        generatePdf();
    }
    private void generatePdf()
    {
        PdfSharp.Pdf.PdfDocument pdfDocument = new PdfSharp.Pdf.PdfDocument();
        pdfDocument.Info.Author = "Philip Johnson";
        pdfDocument.Info.Creator = "Philip Johnson";
        PdfSharp.Pdf.PdfPage pdfPage = pdfDocument.AddPage();
        pdfPage.Width = 684;
        pdfPage.Height = 297;
        PdfSharp.Drawing.XGraphics xGraphics = PdfSharp.Drawing.XGraphics.FromPdfPage(pdfPage);
        var memstr = new System.IO.MemoryStream();
        ((System.Drawing.Image)this.pic).Save(memstr, ImageFormat.Jpeg);
        PdfSharp.Drawing.XImage image = PdfSharp.Drawing.XImage.FromStream(memstr);
        xGraphics.DrawImage(image, 0, 0, 684, 297);

        //PdfSharp.Drawing.XFont xFont = new PdfSharp.Drawing.XFont("Verdana", 20, PdfSharp.Drawing.XFontStyle.BoldItalic);
        //PdfSharp.Drawing.XFont fnt = new PdfSharp.Drawing.XFont("Tahoma", 12);

        //PdfSharp.Drawing.Layout.XTextFormatter tf = new PdfSharp.Drawing.Layout.XTextFormatter(xGraphics);
        //PdfSharp.Drawing.XRect rect = new PdfSharp.Drawing.XRect(100, 10, 250, 220);

        //xGraphics.DrawString(TBRETURNADDRESS.Text, fnt, PdfSharp.Drawing.XBrushes.Black, rect, PdfSharp.Drawing.XStringFormats.TopLeft);
        // Draw the text
        //xGraphics.DrawString("File Format Developer Guide", xFont, PdfSharp.Drawing.XBrushes.Black,
        //    new PdfSharp.Drawing.XRect(0, 0, pdfPage.Width, pdfPage.Height),
        //    PdfSharp.Drawing.XStringFormats.Center);
        // Save the document...
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        pdfDocument.Save(ms);
        string datauri = "data:application/pdf;base64," + Convert.ToBase64String(ms.ToArray());
        this.pdf.Src = datauri;
    }
}