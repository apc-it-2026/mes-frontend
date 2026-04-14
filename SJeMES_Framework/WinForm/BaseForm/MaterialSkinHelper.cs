using MaterialSkin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.WinForm.BaseForm
{
    public class MaterialSkinHelper
    {
        public static MaterialSkinManager MaterialSkinManagerSetDefault(MaterialSkinManager.Themes themes, MaterialSkinManager MSM,MaterialSkin.Controls.MaterialForm MF)
        {
            MSM = MaterialSkinManager.Instance;
            MSM.AddFormToManage(MF);
            MSM.Theme = themes;
            MSM.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            return MSM;
        }
    }
}
