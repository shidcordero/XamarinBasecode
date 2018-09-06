using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinBasecode.Core.CustomRenderer;
using XamarinBasecode.Droid.CustomRenderers;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace XamarinBasecode.Droid.CustomRenderers
{
    internal class CustomDatePickerRenderer : DatePickerRenderer
    {
        public CustomDatePickerRenderer(Android.Content.Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null) return;

            var element = (CustomDatePicker)Element;
            var hasBackground = element.HasBackground;
            
            if (!hasBackground)
            {
                Control.Background = null;
            }
        }
    }
}