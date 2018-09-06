using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinBasecode.Core.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InlineEntry : ContentView
    {
        public static readonly BindableProperty TextValueProperty = BindableProperty.Create(nameof(TextValue),
            typeof(string), typeof(InlineEntry), default(string), BindingMode.OneWayToSource);
        public static readonly BindableProperty LabelValueProperty = BindableProperty.Create(nameof(LabelValue),
            typeof(string), typeof(InlineEntry), default(string), BindingMode.OneWayToSource);

        public InlineEntry()
        {
            InitializeComponent();
        }

        public string LabelValue
        {
            get => (string)GetValue(LabelValueProperty);
            set => SetValue(LabelValueProperty, value);
        }

        public Keyboard KeyBoardInput
        {
            set { EntryFld.Keyboard = value; }
        }

        public string TextValue
        {
            get => (string) GetValue(TextValueProperty);
            set => SetValue(TextValueProperty, value);
        }
        
    }
}