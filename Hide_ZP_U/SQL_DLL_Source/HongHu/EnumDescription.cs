
    using System;
    using System.Collections;
    using System.Reflection;
namespace HongHu
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
    public class EnumDescription : Attribute
    {
        private static Hashtable cachedEnum = new Hashtable();
        private string enumDisplayText;
        private int enumRank;
        private FieldInfo fieldIno;

        public EnumDescription(string enumDisplayText) : this(enumDisplayText, 5)
        {
        }

        public EnumDescription(string enumDisplayText, int enumRank)
        {
            this.enumDisplayText = enumDisplayText;
            this.enumRank = enumRank;
        }

        public static string GetEnumText(Type enumType)
        {
            EnumDescription[] eds = (EnumDescription[]) enumType.GetCustomAttributes(typeof(EnumDescription), false);
            if (eds.Length != 1)
            {
                return string.Empty;
            }
            return eds[0].EnumDisplayText;
        }

        public static string GetFieldText(object enumValue)
        {
            EnumDescription[] descriptions = GetFieldTexts(enumValue.GetType(), SortType.Default);
            foreach (EnumDescription ed in descriptions)
            {
                if (ed.fieldIno.Name == enumValue.ToString())
                {
                    return ed.EnumDisplayText;
                }
            }
            return string.Empty;
        }

        public static EnumDescription[] GetFieldTexts(Type enumType)
        {
            return GetFieldTexts(enumType, SortType.Default);
        }

        public static EnumDescription[] GetFieldTexts(Type enumType, SortType sortType)
        {
            EnumDescription[] descriptions = null;
            if (!cachedEnum.Contains(enumType.FullName))
            {
                FieldInfo[] fields = enumType.GetFields();
                ArrayList edAL = new ArrayList();
                foreach (FieldInfo fi in fields)
                {
                    object[] eds = fi.GetCustomAttributes(typeof(EnumDescription), false);
                    if (eds.Length == 1)
                    {
                        ((EnumDescription) eds[0]).fieldIno = fi;
                        edAL.Add(eds[0]);
                    }
                }
                cachedEnum.Add(enumType.FullName, (EnumDescription[]) edAL.ToArray(typeof(EnumDescription)));
            }
            descriptions = (EnumDescription[]) cachedEnum[enumType.FullName];
            if (descriptions.Length <= 0)
            {
                throw new NotSupportedException("枚举类型[" + enumType.Name + "]未定义属性EnumValueDescription");
            }
            for (int m = 0; m < descriptions.Length; m++)
            {
                if (sortType == SortType.Default)
                {
                    return descriptions;
                }
                for (int n = m; n < descriptions.Length; n++)
                {
                    bool swap = false;
                    switch (sortType)
                    {
                        case SortType.DisplayText:
                            if (string.Compare(descriptions[m].EnumDisplayText, descriptions[n].EnumDisplayText) > 0)
                            {
                                swap = true;
                            }
                            break;

                        case SortType.Rank:
                            if (descriptions[m].EnumRank > descriptions[n].EnumRank)
                            {
                                swap = true;
                            }
                            break;
                    }
                    if (swap)
                    {
                        EnumDescription temp = descriptions[m];
                        descriptions[m] = descriptions[n];
                        descriptions[n] = temp;
                    }
                }
            }
            return descriptions;
        }

        public string EnumDisplayText
        {
            get
            {
                return this.enumDisplayText;
            }
        }

        public int EnumRank
        {
            get
            {
                return this.enumRank;
            }
        }

        public int EnumValue
        {
            get
            {
                return (int) this.fieldIno.GetValue(null);
            }
        }

        public string FieldName
        {
            get
            {
                return this.fieldIno.Name;
            }
        }

        public enum SortType
        {
            Default,
            DisplayText,
            Rank
        }
    }
}

