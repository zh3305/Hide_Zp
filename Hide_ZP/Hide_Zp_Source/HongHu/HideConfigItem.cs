namespace HongHu
{
    using System;
    using System.Collections.Generic;
    
/// <summary>
/// ��������
/// </summary>
    public class HideConfigItem
    {
        /// <summary>
        /// �Ƿ���ʾ������
        /// </summary>
        public bool Jdt = true;
        /// <summary>
      /// �ƶ�·��
        /// </summary>
        public string Movetarget = "";
        /// <summary>
        /// ���ط�ʽ
        /// </summary>
        public Runfs Rf = Runfs.Login;
        /// <summary>
        /// ��ݼ�
        /// </summary>
        public List<Kuaijijian> UHotkeys = new List<Kuaijijian>();
        /// <summary>
        /// ��ݼ���ʾ����
        /// </summary>
        public Kuaijijian Xszt;
        /// <summary>
        /// �Ƿ���ʾСͼ��
        /// </summary>
        public bool Xtb = true;
        /// <summary>
        /// ��ݼ���������
        /// </summary>
        public Kuaijijian Yczt;
        /// <summary>
        /// 
        /// </summary>
        public Kuaijijian Zjm;
        /// <summary>
        /// �Ƿ���ʾ������
        /// </summary>
        public bool ZRun = false;
    }
}

