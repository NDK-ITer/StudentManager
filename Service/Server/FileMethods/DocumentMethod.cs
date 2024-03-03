using Microsoft.IdentityModel.Tokens;
using Xceed.Document.NET;
using Xceed.Words.NET;
using GemBox.Document;
using DocumentFormat.OpenXml.Packaging;
using HtmlAgilityPack;


namespace Server.FileMethods
{
    public class DocumentMethod : GenericFileMethod
    {
        public DocumentMethod(IWebHostEnvironment env) : base(env)
        {
        }


        public string ConvertToHtml(string folder, string fileName, string baseUrl)
        {
            if (folder.IsNullOrEmpty() || fileName.IsNullOrEmpty()) { return string.Empty; }
            var contentPath = this.environment.ContentRootPath;
            var path = Path.Combine(contentPath, folder);
            var fileWithPath = Path.Combine(path, fileName);

            if (!File.Exists(fileWithPath))
            {
                throw new FileNotFoundException("File not found.", fileWithPath);
            }

            var htmlContent = new StringWriter();
            var tempImageFolderPath = Path.Combine(contentPath, "TempImages");

            if (!Directory.Exists(tempImageFolderPath))
            {
                Directory.CreateDirectory(tempImageFolderPath);
            }

            var tempImageFiles = new List<string>();

            using (DocX doc = DocX.Load(fileWithPath))
            {
                foreach (Xceed.Document.NET.Paragraph paragraph in doc.Paragraphs)
                {
                    foreach (var pic in paragraph.Pictures)
                    {
                        var imageName = Guid.NewGuid().ToString() + ".png";
                        var imagePath = Path.Combine(tempImageFolderPath, imageName);

                        using (FileStream fs = new FileStream(imagePath, FileMode.Create))
                        {
                            pic.Stream.CopyTo(fs);
                        }

                        string imageUrl = new Uri(new Uri(baseUrl.Trim()), $"public/{imageName}").AbsoluteUri;
                        htmlContent.WriteLine($"<img src='{imageUrl}'/>");

                        tempImageFiles.Add(imageName);
                    }

                    htmlContent.WriteLine($"<p>{paragraph.Text.Trim()}</p>");
                }
            }


            return htmlContent.ToString();
        }

    }
}
