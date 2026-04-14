using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.Common
{
    /// <summary>
    /// /*系统枚举*/
    /// </summary>
    public class ENUM
    {
    }

    /// <summary>
    /// 页码/每页显示记录数
    /// </summary>
    public class enum_page
    {
        /// <summary>
        /// 页码
        /// </summary>
        public const string PageIndex = "0";

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        public const string enum_PageSize = "15";

    }


    /// <summary>
    /// 是/否
    /// </summary>
    public enum enum_whether
    {
        /// <summary>
        /// 是
        /// </summary>
        TRUE,
        /// <summary>
        /// 否
        /// </summary>
        FALSE
    }

    /// <summary>
    /// 引用等级
    /// </summary>
    public enum enum_ref_level
    {
        /// <summary>
        /// ART
        /// </summary>
        ART,
        /// <summary>
        /// ART
        /// </summary>
        MW
    }

    /// <summary>
    /// 公式类型
    /// </summary>
    public class enum_formula_type
    {
        /// <summary>
        /// 通用
        /// </summary>
        public const string enum_formula_type_0 = "0";

        /// <summary>
        /// 自定义
        /// </summary>
        public const string enum_formula_type_1 = "1";

    }

    /// <summary>
    /// 测试项目 — 检测标准类型
    /// </summary>
    public class enum_testitem_type
    {
        /// <summary>
        /// 固定值
        /// </summary>
        public const string enum_testitem_type_1 = "1";
        /// <summary>
        /// 上下限
        /// </summary>
        public const string enum_testitem_type_2 = "2";
        /// <summary>
        /// 误差值
        /// </summary>
        public const string enum_testitem_type_3 = "3";
    }
    /// <summary>
    /// 确认鞋管理判断状态
    /// </summary>
    public class enum_confirm_status
    {
        /// <summary>
        /// 在期内
        /// </summary>
        public const string enum_confirm_status_0 ="0";
        /// <summary>
        ///  在期内
        /// </summary>
        public const string enum_confirm_status_string_0 = "在期内";
        /// <summary>
        /// 已忽略
        /// </summary>
        public const string enum_confirm_status_1 = "1";
        /// <summary>
        ///  已忽略
        /// </summary>
        public const string enum_confirm_status_string_1 = "已忽略";
        /// <summary>
        /// 过期
        /// </summary>
        public const string enum_confirm_status_2 = "2";
        /// <summary>
        /// 过期
        /// </summary>
        public const string enum_confirm_status_string_2 = "过期";
        /// <summary>
        /// 报废
        /// </summary>
        public const string enum_confirm_status_3 = "3";
        /// <summary>
        /// 报废
        /// </summary>
        public const string enum_confirm_status_string_3 = "报废";
    }
    /// <summary>
    /// 通用公式
    /// </summary>
    public class enum_general_formula
    {
        /// <summary>
        /// 平均值
        /// </summary>
        public const string enum_general_formula_0 = "0";

        /// <summary>
        /// 最大值
        /// </summary>
        public const string enum_general_formula_1 = "1";

        /// <summary>
        /// 最小值
        /// </summary>
        public const string enum_general_formula_2 = "2";

        /// <summary>
        /// 极差值
        /// </summary>
        public const string enum_general_formula_3 = "3";

    }

    /// <summary>
    /// 检测项目分类
    /// </summary>
    public class enum_testitem_category
    {
        /// <summary>
        /// 测试项目
        /// </summary>
        public const string enum_testitem_category_1 = "1";

        /// <summary>
        /// 外观检测项目
        /// </summary>
        public const string enum_testitem_category_2 = "2";

        /// <summary>
        /// 试穿检测项目
        /// </summary>
        public const string enum_testitem_category_3 = "3";


    }

    public class enum_qa_file_type
    {
        /// <summary>
        /// 文件类型Limited release
        /// </summary>
        public const string enum_qa_file_type_0 = "1";

        /// <summary>
        /// 文件类型Disclimer
        /// </summary>
        public const string enum_qa_file_type_1 = "2";
        /// <summary>
        /// 文件类型Visual standard
        /// </summary>
        public const string enum_qa_file_type_2 = "3";
        /// <summary>
        /// 文件类型Other
        /// </summary>
        public const string enum_qa_file_type_3 = "4";
    }

    /// <summary>
    /// 单据类型
    /// </summary>
    public class enum_document_type
    {
        /// <summary>
        /// PO单号
        /// </summary>
        public const string enum_document_type_0 = "0";

        /// <summary>
        /// 收料单号
        /// </summary>
        public const string enum_document_type_1 = "1";
    }
    /// <summary>
    /// 原材料画皮等级
    /// </summary>
    public class enum_paintedskin_level
    {
        /// <summary>
        /// 一级
        /// </summary>
        public const string enum_barcode_print_type_1 = "1";
        /// <summary>
        /// 二级
        /// </summary>
        public const string enum_barcode_print_type_2 = "2";
        /// <summary>
        /// 三级
        /// </summary>
        public const string enum_barcode_print_type_3 = "3";
        /// <summary>
        /// 四级
        /// </summary>
        public const string enum_barcode_print_type_4 = "4";
        /// <summary>
        /// 五级
        /// </summary>
        public const string enum_barcode_print_type_5 = "5";
        /// <summary>
        /// 六级
        /// </summary>
        public const string enum_barcode_print_type_6 = "6";
        /// <summary>
        /// 六级以下
        /// </summary>
        public const string enum_barcode_print_type_6B = "6B";
    }

    /// <summary>
    /// 上传图片/文件 路径枚举
    /// </summary>
    public enum enum_filepath1
    {
        /// <summary>
        /// 实验室送测提交图片
        /// </summary>
        enum_filepath_1 = 1,

        /// <summary>
        /// ART图片上传
        /// </summary>
        enum_filepath_2 = 2,

        /// <summary>
        /// ART定制检验项目文件路径
        /// </summary>
        enum_filepath_3 = 3,

        /// <summary>
        /// QA鞋型问题点图片上传路径
        /// </summary>
        enum_filepath_4 = 4,

        /// <summary>
        /// QA鞋型 文件上传 四种类型（Limited release/Disclimer/Visual standard/Other）
        /// </summary>
        enum_filepath_5 = 5,

        /// <summary>
        /// 金属检验上传图片
        /// </summary>
        enum_filepath_6 = 6,

        /// <summary>
        /// 色卡检验项目图片
        /// </summary>
        enum_filepath_7 = 7,

        /// <summary>
        /// 鞋面品质审核图片上传路径
        /// </summary>
        enum_filepath_8 = 8,

        /// <summary>
        /// 发外厂商品质体系图片上传路径
        /// </summary>
        enum_filepath_9 = 9,

        /// <summary>
        /// 平板抽检图片上传路径
        /// </summary>
        enum_filepath_10 = 10,

        /// <summary>
        /// ART文件绑定
        /// </summary>
        enum_filepath_11 = 11,

        /// <summary>
        /// 重检报告
        /// </summary>
        enum_filepath_12 = 12,

        /// <summary>
        /// 量产试作图片
        /// </summary>
        enum_filepath_13 = 13,

        /// <summary>
        /// 异常呈报单
        /// </summary>
        enum_filepath_14 = 14,

        /// <summary>
        /// 不良退货图片
        /// </summary>
        enum_filepath_15 = 15,

        /// <summary>
        /// 客户投诉图片上传路径
        /// </summary>
        enum_filepath_16 = 16,

        /// <summary>
        /// 客户投诉文件上传路径
        /// </summary>
        enum_filepath_17 = 17,

    }
    /// <summary>
    /// 窗体计算公式维护(中/英/越)
    /// </summary>
    public class Formula_Type_enum
    {
        /// <summary>
        /// 通用
        /// </summary>
        public const string Type_enum_0 = "通用";
        /// <summary>
        /// 自定义
        /// </summary>
        public const string Type_enum_1 = "自定义";
        /// <summary>
        /// 退格
        /// </summary>
        public const string Type_enum_2 = "退格";
        /// <summary>
        /// 清空
        /// </summary>
        public const string Type_enum_3 = "清空";
        /// <summary>
        /// 输入值N
        /// </summary>
        public const string Type_enum_4 = "输入值N";



    }
    public class ENUM_JUDGMENT_CRITERIA_CODE
    {
        public const string JUDGMENT_CODE_0 = "0";
        public const string JUDGMENT_CODE_1 = "1";
        public const string JUDGMENT_CODE_2 = "2";
        public const string JUDGMENT_CODE_3 = "3";
    }
    public class ENUM_JUDGMENT_CRITERIA
    {
        public const string JUDGMENT_0 = ">";
        public const string JUDGMENT_1 = "<";
        public const string JUDGMENT_2 = ">=";
        public const string JUDGMENT_3 = "<=";
    }
}
