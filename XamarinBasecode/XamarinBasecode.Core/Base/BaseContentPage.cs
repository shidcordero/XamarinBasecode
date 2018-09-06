using MvvmCross.Forms.Views;
using Xamarin.Forms;
using XamarinBasecode.Core.Utilities;

namespace XamarinBasecode.Core.Base
{
    public class BaseContentPage : MvxContentPage
    {
        protected RelativeLayout CreateLoadingIndicator()
        {
            var loadingIndicator = new ActivityIndicator
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Start,
                Scale = 1,
                Color = Color.Blue
            };
            loadingIndicator.SetBinding(ActivityIndicator.IsRunningProperty, Constants.Common.LoadingKey);

            var container = new RelativeLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Black,
                Opacity = 0.6
            };

            container.Children.Add(loadingIndicator,
                Constraint.RelativeToParent((parent) => (parent.Width / 2) - 16),
                Constraint.RelativeToParent((parent) => (parent.Height / 2) - 16));

            container.SetBinding(IsVisibleProperty, Constants.Common.LoadingKey);
            container.SetBinding(ActivityIndicator.IsRunningProperty, Constants.Common.LoadingKey);

            return container;
        }

        // NOTE: can get the loading indicator centered with RelativeLayout, but then other elements/layout behave/look strange ...
        protected RelativeLayout CreateLoadingIndicatorRelativeLayout(View content)
        {
            var overlay = new RelativeLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var loadingIndicator = CreateLoadingIndicator();
            overlay.Children.Add(content, Constraint.Constant(0), Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent((parent) => parent.Width),
                heightConstraint: Constraint.RelativeToParent((parent) => parent.Height));
            overlay.Children.Add(loadingIndicator, Constraint.Constant(0), Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent((parent) => parent.Width),
                heightConstraint: Constraint.RelativeToParent((parent) => parent.Height));

            return overlay;
        }

        // NOTE: can get the loading indicator centered with AbsoluteLayout, but then other elements/layout behave/look strange ...
        protected AbsoluteLayout CreateLoadingIndicatorAbsoluteLayout(View content)
        {
            var overlay = new AbsoluteLayout();
            var loadingIndicator = CreateLoadingIndicator();

            AbsoluteLayout.SetLayoutFlags(content, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(content, new Rectangle(0f, 0f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(loadingIndicator, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(loadingIndicator, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            overlay.Children.Add(content);
            overlay.Children.Add(loadingIndicator);

            return overlay;
        }
    }
}
