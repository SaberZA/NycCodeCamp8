using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using CodeCamp.Core.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;

namespace CodeCamp.App.iOS.Views
{
    public class MenuView : DialogViewControllerBase
    {
        public MenuView()
            : base(UITableViewStyle.Plain)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var frame = View.Frame;
            frame.Width = 200;
            View.Frame = frame;

            TableView.ScrollEnabled = false;

            Func<string, StyledStringElement> createElement = text =>
                new StyledStringElement(text) 
                { 
                    Accessory = UITableViewCellAccessory.DisclosureIndicator,
                    ShouldDeselectAfterTouch = true
                };

            StyledStringElement overviewElement = createElement("Overview"),
                                sessionsElement = createElement("Full Schedule"),
                                speakersElement = createElement("Speakers"),
                                mapElement = createElement("Map");

            var binder = this.CreateBindingSet<MenuView, MenuViewModel>();
            binder.Bind(overviewElement)
                  .For(el => el.SelectedCommand)
                  .To(vm => vm.ShowOverviewCommand);
            binder.Bind(sessionsElement)
                  .For(el => el.SelectedCommand)
                  .To(vm => vm.ShowSessionsCommand);
            binder.Bind(speakersElement)
                  .For(el => el.SelectedCommand)
                  .To(vm => vm.ShowSpeakersCommand);
            binder.Bind(mapElement)
                  .For(el => el.SelectedCommand)
                  .To(vm => vm.ShowMapCommand);
            binder.Apply();

            Root = new RootElement("")
            {
                new Section
                {
                    overviewElement, sessionsElement, speakersElement, mapElement
                }
            };
        }

        private new MenuViewModel ViewModel
        {
            get { return (MenuViewModel)base.ViewModel; }
        }
    }
}