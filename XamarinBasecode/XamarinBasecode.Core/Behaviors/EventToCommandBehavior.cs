using System;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBasecode.Core.Base;
using XamarinBasecode.Core.Utilities;

namespace XamarinBasecode.Core.Behaviors
{
    public class EventToCommandBaseBehavior : BaseBehavior<View>
    {
        private Delegate _eventHandler;

        public static readonly BindableProperty EventNameProperty = BindableProperty.Create(Constants.Behavior.EventName, typeof(string), typeof(EventToCommandBaseBehavior), null, propertyChanged: OnEventNameChanged);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(Constants.Behavior.Command, typeof(ICommand), typeof(EventToCommandBaseBehavior));
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(Constants.Behavior.CommandParameter, typeof(object), typeof(EventToCommandBaseBehavior));
        public static readonly BindableProperty InputConverterProperty = BindableProperty.Create(Constants.Behavior.Converter, typeof(IValueConverter), typeof(EventToCommandBaseBehavior));

        public string EventName
        {
            get => (string)GetValue(EventNameProperty);
            set => SetValue(EventNameProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public IValueConverter Converter
        {
            get => (IValueConverter)GetValue(InputConverterProperty);
            set => SetValue(InputConverterProperty, value);
        }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            RegisterEvent(EventName);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            DeregisterEvent(EventName);
            base.OnDetachingFrom(bindable);
        }

        private void RegisterEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);
            if (eventInfo == null)
            {
                throw new ArgumentException($"EventToCommandBaseBehavior: Can't register the '{EventName}' event.");
            }
            var methodInfo = typeof(EventToCommandBaseBehavior).GetTypeInfo().GetDeclaredMethod(Constants.Behavior.OnEvent);
            _eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
            eventInfo.AddEventHandler(AssociatedObject, _eventHandler);
        }

        private void DeregisterEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            if (_eventHandler == null)
            {
                return;
            }

            var eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);
            if (eventInfo == null)
            {
                throw new ArgumentException($"EventToCommandBaseBehavior: Can't de-register the '{EventName}' event.");
            }
            eventInfo.RemoveEventHandler(AssociatedObject, _eventHandler);
            _eventHandler = null;
        }

        private void OnEvent(object sender, object eventArgs)
        {
            if (Command == null)
            {
                return;
            }

            object resolvedParameter;
            if (CommandParameter != null)
            {
                resolvedParameter = CommandParameter;
            }
            else if (Converter != null)
            {
                resolvedParameter = Converter.Convert(eventArgs, typeof(object), null, null);
            }
            else
            {
                resolvedParameter = eventArgs;
            }

            if (Command.CanExecute(resolvedParameter))
            {
                Command.Execute(resolvedParameter);
            }
        }

        private static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (EventToCommandBaseBehavior)bindable;
            if (behavior.AssociatedObject == null)
            {
                return;
            }

            var oldEventName = (string)oldValue;
            var newEventName = (string)newValue;

            behavior.DeregisterEvent(oldEventName);
            behavior.RegisterEvent(newEventName);
        }
    }
}