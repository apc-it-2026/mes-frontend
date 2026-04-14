using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace SJeMES_Framework.Common
{
    public class QRCode
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="content"></param>
        /// <param name="path">保存路径(可为空,如有指定地址建议后缀为jpg格式)</param>
        /// <param name="qrCodeEncodeMode">编码模式,包括A、B、N，默认为B</param>
        /// <param name="qrCodeErrorCorrect">纠错率,包括L（7%）、M（15%）、Q（25%）和H（30%）,默认为M级</param>
        /// <param name="qrCodeVersion">版本号,介于1-40.版本越高信息容量越大,默认设为8</param>
        /// <param name="qrCodeScale">比例,默认4</param>
        /// <returns>返回二维码生成地址</returns>
        public static string CreateQRCodePath(string content, string path = "", string fileName = "", string qrCodeEncodeMode = "B", string qrCodeErrorCorrect = "M", int qrCodeVersion = 8, int qrCodeScale = 4)
        {
            Image image = CreateQRCode(content, qrCodeEncodeMode, qrCodeErrorCorrect, qrCodeVersion, qrCodeScale);





            string filepath = string.Empty;

            if (!string.IsNullOrEmpty(path))
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filepath = path;
            }

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = DateTime.Now.ToString("yyyymmddhhmmssfff").ToString() + ".png";

            }
            filepath = filepath + "\\" + fileName;
            FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
            image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);

            fs.Close();
            image.Dispose();

            return "/Content/QRCode/" + fileName;
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="content"></param>
        /// <param name="path">保存路径(可为空,如有指定地址建议后缀为jpg格式)</param>
        /// <param name="qrCodeEncodeMode">编码模式,包括A、B、N，默认为B</param>
        /// <param name="qrCodeErrorCorrect">纠错率,包括L（7%）、M（15%）、Q（25%）和H（30%）,默认为M级</param>
        /// <param name="qrCodeVersion">版本号,介于1-40.版本越高信息容量越大,默认设为8</param>
        /// <param name="qrCodeScale">比例,默认4</param>
        /// <returns></returns>
        public static Image CreateQRCode(string content, string qrCodeEncodeMode = "B", string qrCodeErrorCorrect = "M", int qrCodeVersion = 8, int qrCodeScale = 8)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

            qrCodeEncodeMode = qrCodeEncodeMode.ToUpper();
            switch (qrCodeEncodeMode)
            {
                case "B":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
                case "A":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                    break;
                case "N":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                    break;
                default:
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
            }

            qrCodeEncoder.QRCodeScale = qrCodeScale;
            qrCodeEncoder.QRCodeVersion = qrCodeVersion;

            qrCodeErrorCorrect = qrCodeErrorCorrect.ToUpper();
            switch (qrCodeErrorCorrect)
            {
                case "L":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                    break;
                case "M":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                    break;
                case "Q":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                    break;
                default:
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                    break;
            }

            Image image = qrCodeEncoder.Encode(content);

            return image;
        }

        /// <summary>
        /// 二维码解码
        /// </summary>
        /// <param name="filePath">图片路径</param>
        /// <returns></returns>
        public static string CodeDecoder(string filePath)
        {
            if (!File.Exists(filePath))
                return null;
            Bitmap myBitmap = new Bitmap(Image.FromFile(filePath));
            QRCodeDecoder decoder = new QRCodeDecoder();
            string decodedString = decoder.decode(new QRCodeBitmapImage(myBitmap));
            return decodedString;
        }
    }
}
