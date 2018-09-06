using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinBasecode.Core.CustomRenderer;
using XamarinBasecode.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace XamarinBasecode.iOS.CustomRenderers
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null) return;
            
            var element = (CustomDatePicker)Element;
            var hasBackground = element.HasBackground;

            if (hasBackground)
            {
                Control.Background = null;
            }
        }
    }
}