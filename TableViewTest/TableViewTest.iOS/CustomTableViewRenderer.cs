using System;
using TableViewTest;
using Foundation;
using TableViewTest.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using CoreGraphics;

[assembly: ExportRenderer(typeof(CustomTableView), typeof(CustomTableViewRenderer))]
namespace TableViewTest.iOS
{
    public class CustomTableViewRenderer : TableViewRenderer
    {
        public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            Control.LayoutIfNeeded();
            var size = new Size(Control.ContentSize.Width, Control.ContentSize.Height);
            return new SizeRequest(size);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);

            if(e.NewElement is CustomTableView view)
            {
                Control.ScrollEnabled = false;
                SetSource();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var view = (CustomTableView)Element;
            if(e.PropertyName == TableView.HasUnevenRowsProperty.PropertyName)
            {
                SetSource();
            }
        }

        private void SetSource()
        {
            var view = (CustomTableView)Element;
            if(view.NoFooter || view.NoHeader)
            {
                Control.Source = new CustomTableViewModelRenderer(view);
            }
            else
            {
                Control.Source = Element.HasUnevenRows ? new UnEvenTableViewModelRenderer(Element) :
                    new TableViewModelRenderer(Element);
            }
        }

        public class CustomTableViewModelRenderer : UnEvenTableViewModelRenderer
        {
            private readonly CustomTableView _view;

            public CustomTableViewModelRenderer(CustomTableView model)
                : base(model)
            {
                _view = model;
            }

            public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            {
                if(_view.HasUnevenRows)
                {
                    return UITableView.AutomaticDimension;
                }

                return base.GetHeightForRow(tableView, indexPath);
            }

            public override nfloat GetHeightForHeader(UITableView tableView, nint section)
            {
                if(_view.NoHeader)
                {
                    return 0.00001f;
                }

                return base.GetHeightForHeader(tableView, section);
            }

            public override UIView GetViewForHeader(UITableView tableView, nint section)
            {
                if(_view.NoHeader)
                {
                    return new UIView(new CGRect(0, 0, 0, 0));
                }

                return base.GetViewForHeader(tableView, section);
            }

            public override nfloat GetHeightForFooter(UITableView tableView, nint section)
            {
                if(_view.NoFooter)
                {
                    return 0.00001f;
                }

                return 10f;
            }

            public override UIView GetViewForFooter(UITableView tableView, nint section)
            {
                if(_view.NoFooter)
                {
                    return new UIView(new CGRect(0, 0, 0, 0));
                }

                return base.GetViewForFooter(tableView, section);
            }
        }
    }

}