#region 反向方案

using System;
using System.Collections.Generic;
using System.Text;

namespace HongHu.MyUtils.Day
{
    public class Program
    {
        public static void Mains()
        {
            NameConverter nc = new NameConverter();
            nc.AddInnerNameConverters(
                new ChineseFullNameConverter(),
                new ChineseShortNameConverter(),
                new EnglishFullNameConverter(),
                new EnglishShortNameConverter(),
                new EnglishSingleLetterConverter()
            );
            Console.WriteLine(nc.ToString(DateTime.Now.DayOfWeek));
        }
    }

    public abstract class NameConverterBase : IComparable
    {
        public abstract string ToString(DayOfWeek DayOfWeek);

       // IComparable Members;
        #region IComparable Members

        int IComparable.CompareTo(object obj)
        {
            return GetType().ToString().CompareTo(obj.GetType().ToString());
        }

        #endregion
    }

    public class NameConverter
    {
        private StringBuilder m_Content = new StringBuilder();

        private List<NameConverterBase> m_NameConverters = new List<NameConverterBase>();

        public void AddInnerNameConverter(NameConverterBase innerNameConverter)
        {
            if (m_NameConverters.Contains(innerNameConverter))
            {
                return;
            }

            m_NameConverters.Add(innerNameConverter);
        }

        public void AddInnerNameConverters(IEnumerable<NameConverterBase> innerNameConverters)
        {
            foreach (NameConverterBase innerNameConverter in innerNameConverters)
            {
                AddInnerNameConverter(innerNameConverter);
            }
        }

        public void AddInnerNameConverters(params NameConverterBase[] innerNameConverters)
        {
            foreach (NameConverterBase innerNameConverter in innerNameConverters)
            {
                AddInnerNameConverter(innerNameConverter);
            }
        }

        public string ToString(DayOfWeek day)
        {
            foreach (NameConverterBase nameConverter in m_NameConverters)
            {
                m_Content.AppendFormat("{0} ", nameConverter.ToString(day));
            }

            return m_Content.ToString().TrimEnd();
        }
    }

    public class ChineseFullNameConverter : NameConverterBase
    {
        public override string ToString(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Sunday:
                    return "星期日";
                case DayOfWeek.Monday:
                    return "星期一";;
                case DayOfWeek.Tuesday:
                    return "星期二";;
                case DayOfWeek.Wednesday:
                    return "星期三";;
                case DayOfWeek.Thursday:
                    return "星期四";;
                case DayOfWeek.Friday:
                    return "星期五";;
                case DayOfWeek.Saturday:
                    return "星期六";;
                default:
                    throw new ArgumentOutOfRangeException("day", "Value not supported!");
            }
        }
    }

    public class ChineseShortNameConverter : NameConverterBase
    {
        public override string ToString(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Sunday:
                    return "日";
                case DayOfWeek.Monday:
                    return "一";
                case DayOfWeek.Tuesday:
                    return "二";
                case DayOfWeek.Wednesday:
                    return "三";
                case DayOfWeek.Thursday:
                    return "四";
                case DayOfWeek.Friday:
                    return "五";
                case DayOfWeek.Saturday:
                    return "六";
                default:
                    throw new ArgumentOutOfRangeException("day", "Value not supported!");
            }
        }
    }

    public class EnglishFullNameConverter : NameConverterBase
    {
        public override string ToString(DayOfWeek day)
        {
            return day.ToString();
        }
    }

    public class EnglishShortNameConverter : NameConverterBase
    {
        public override string ToString(DayOfWeek day)
        {
            return day.ToString().Substring(0, 3);
        }
    }

    public class EnglishSingleLetterConverter : NameConverterBase
    {
        public override string ToString(DayOfWeek day)
        {
            return day.ToString().Substring(0, 1);
        }
    }
}

#endregion