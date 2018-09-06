using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;
using XamarinBasecode.Core.CustomRenderer;
using XamarinBasecode.UWP.CustomRenderers;
using DatePicker = Xamarin.Forms.DatePicker;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace XamarinBasecode.UWP.CustomRenderers
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