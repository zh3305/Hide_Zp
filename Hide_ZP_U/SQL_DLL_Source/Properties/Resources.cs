namespace HongHu.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
    internal class Resources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resources()
        {
        }

        internal static Bitmap btn_close_down
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("btn_close_down", resourceCulture);
            }
        }

        internal static Bitmap btn_close_highlight
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("btn_close_highlight", resourceCulture);
            }
        }

        internal static Bitmap btn_close_normal
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("btn_close_normal", resourceCulture);
            }
        }

        internal static Bitmap btn_mini_down
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("btn_mini_down", resourceCulture);
            }
        }

        internal static Bitmap btn_mini_highlight
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("btn_mini_highlight", resourceCulture);
            }
        }

        internal static Bitmap btn_mini_normal
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("btn_mini_normal", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static Bitmap loading
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("loading", resourceCulture);
            }
        }

        internal static Bitmap loading2
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("loading2", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("HongHu.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
    }
}

