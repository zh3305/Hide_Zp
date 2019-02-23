using System;
namespace HongHu
{
    #region enum Runfs �������ģʽ
    [EnumDescription("�������ģʽ")]
    public enum Runfs
    {
        /// <summary>
        /// δ����
        /// </summary>
        [EnumDescription("δ����")]
        None = 0,
        /// <summary>
        /// ��¼����
        /// </summary>
        [EnumDescription("��¼����")]
        Login = 1,
        /// <summary>
        /// ϵͳ����
        /// </summary>
        [EnumDescription("ϵͳ����")]
        System = 2,
        /// <summary>
        /// ��������
        /// </summary>
        [EnumDescription("��������")]
        Detach = 3,
        /// <summary>
        /// �ƶ�����
        /// </summary>
        [EnumDescription("�ƶ�����")]
        Move = 4,
        /// <summary>
        /// �ƶ�����
        /// </summary>
        [EnumDescription("�ƶ�����")]
        RunOnU = 5
    }

    #endregion Runfs �������ģʽ

    /// <summary>
    /// ���̲߳���
    /// </summary>
    public class HongHUWorkArgs
    {
        #region ���캯��
        public HongHUWorkArgs() { }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="ewk">WorkDoEnumWork</param>
        /// <param name="e">��ز���</param>
        public HongHUWorkArgs(EnumWork ewk, object e)
        {
            WorkDoEnumWork = ewk;
            Args = e;
        }
        #endregion  ���캯��

        #region ����ö��

        /// <summary>
        /// ����ö��
        /// </summary>
        public enum EnumWork
        {
            /// <summary>
            /// ˢ��������Ϣ
            /// </summary>
            SXZT,
            /// <summary>
            /// ��ʾ���ز���
            /// </summary>
            XSYC
        }

        #endregion ����ö��

        #region ����
        /// <summary>
        /// ��ز���
        /// </summary>
        public object Args;
        /// <summary>
        /// WorkDoEnumWork
        /// </summary>
        public EnumWork WorkDoEnumWork;
        #endregion ����
    }
}

