using System;
using System.Reflection;
using Xamarin.Forms;

namespace TableViewTest
{
    public class TablePage : ContentPage
    {
        public TablePage()
        {
            var table1 = new PageTableView
            {
                BackgroundColor = Color.Cyan,
                Root = new TableRoot
                {
                    new TableSection("Table 1")
                    {
                        new LabelEntryTableCell("Label A"),
                        new LabelEntryTableCell("Label B")
                    }
                }
            };

            var label1 = new Label
            {
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                    "Vivamus accumsan lacus orci. Nulla in enim erat.",
                Margin = new Thickness(15, 5, 15, 0),
                BackgroundColor = Color.Yellow
            };

            var table2 = new PageTableView
            {
                BackgroundColor = Color.Red,
                Root = new TableRoot
                {
                    new TableSection("Table 2")
                    {
                        new LabelEntryTableCell("Label C"),
                        new LabelEntryTableCell("Label D")
                    }
                }
            };

            var label2 = new Label
            {
                Text = "Duis vulputate mattis elit. " +
                    "Donec vulputate lorem vitae elit posuere, quis consequat ligula imperdiet.",
                Margin = new Thickness(15, 5, 15, 0),
                BackgroundColor = Color.Green
            };

            var stackLayout = new StackLayout
            {
                Children = { table1, label1, table2, label2 },
                Spacing = 0
            };

            BackgroundColor = Color.Gray;
            Title = "Custom Table View";
            Content = new ScrollView
            {
                Content = stackLayout
            };
        }
    }

    public class PageTableView : CustomTableView
    {
        public PageTableView()
        {
            Intent = TableIntent.Settings;
            HasUnevenRows = true;
            RowHeight = -1;
            VerticalOptions = LayoutOptions.Start;
            NoFooter = true;
        }
    }

    public class CustomTableView : TableView
    {
        public bool NoHeader { get; set; }
        public bool NoFooter { get; set; }

        protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
        {
            if(!VerticalOptions.Expands)
            {
                var baseOnSizeRequest = GetVisualElementOnSizeRequest();
                return baseOnSizeRequest(widthConstraint, heightConstraint);
            }

            return base.OnSizeRequest(widthConstraint, heightConstraint);
        }

        public Func<double, double, SizeRequest> GetVisualElementOnSizeRequest()
        {
            var handle = typeof(VisualElement).GetMethod(
                "OnSizeRequest",
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new Type[] { typeof(double), typeof(double) },
                null)?.MethodHandle;

            var pointer = handle.Value.GetFunctionPointer();
            return (Func<double, double, SizeRequest>)Activator.CreateInstance(
                typeof(Func<double, double, SizeRequest>), this, pointer);
        }
    }

    public class LabelEntryTableCell : ViewCell
    {
        public LabelEntryTableCell(string labelText)
        {
            var label = new Label { Text = labelText };
            var entry = new Entry();

            View = new StackLayout
            {
                Children = { label, entry },
                BackgroundColor = Color.White,
                Padding = 10
            };
        }
    }
}
