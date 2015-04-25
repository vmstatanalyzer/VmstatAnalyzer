using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace VmstatAnalyzer.Utility
{
    public class ImageBuilder
    {
        public void CopyToClipboard(Chart chart)
        {
            MemoryStream stream = new MemoryStream();
            chart.SaveImage(stream, ChartImageFormat.Bmp);
            Bitmap bitmap = new Bitmap(stream);
            Clipboard.SetDataObject(bitmap);
        }

        public void ExportToImage(Chart chart)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Bitmap (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png|GIF (*.gif)|*.gif|TIFF (*.tif)|*.tif|EMF (*.emf)|*.emf|EMF-Plus (*.emf)|*.emf|EMF-Dual (*.emf)|*.emf";
            dialog.RestoreDirectory = true;
            dialog.FileName = DateTime.Now.ToString("yyyyMMdd_hhmmss");

            if (DialogResult.OK == dialog.ShowDialog())
            {
                string fileName = dialog.FileName;
                ChartImageFormat imageFormat = ChartImageFormat.Bmp;

                switch (dialog.FilterIndex)
                {
                    case 0:
                        imageFormat = ChartImageFormat.Bmp;
                        break;

                    case 1:
                        imageFormat = ChartImageFormat.Jpeg;
                        break;

                    case 2:
                        imageFormat = ChartImageFormat.Png;
                        break;

                    case 3:
                        imageFormat = ChartImageFormat.Gif;
                        break;

                    case 4:
                        imageFormat = ChartImageFormat.Tiff;
                        break;

                    case 5:
                        imageFormat = ChartImageFormat.Emf;
                        break;

                    case 6:
                        imageFormat = ChartImageFormat.EmfPlus;
                        break;

                    case 7:
                        imageFormat = ChartImageFormat.EmfDual;
                        break;

                    default:
                        imageFormat = ChartImageFormat.Bmp;
                        break;
                }

                chart.SaveImage(fileName, imageFormat);
            }
        }
    }
}
