using XamarinBasecode.Core.Base;

namespace XamarinBasecode.Core.Views
{
    public partial class SecondPage : BaseContentPage
    {
        public SecondPage()
        {
            InitializeComponent();

            //initialize only if needed Activity Indicator
            var tempContent = Content;

            //Assign content to null to fix bug for iOS
            Content = null;
            Content = CreateLoadingIndicatorRelativeLayout(tempContent);
        }
    }
}
