using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace ACEOMM.UI.Behaviors
{
    public class BindableSelectedItemBehavior : Behavior<TreeView>
    {
        #region SelectedItem Property

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public object ParentItem
        {
            get { return GetValue(ParentItemProperty); }
            set { SetValue(ParentItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(BindableSelectedItemBehavior), new UIPropertyMetadata(null, OnSelectedItemChanged));

        public static readonly DependencyProperty ParentItemProperty =
            DependencyProperty.Register("ParentItem", typeof(object), typeof(BindableSelectedItemBehavior), new UIPropertyMetadata(null, OnSelectedItemChanged));

        private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var item = e.NewValue as TreeViewItem;
            if (item != null)
            {
                item.SetValue(TreeViewItem.IsSelectedProperty, true);
            }
        }

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.SelectedItemChanged += OnTreeViewSelectedItemChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
            }
        }

        private static TreeViewItem ContainerFromItem(ItemContainerGenerator containerGenerator, object item)
        {
            TreeViewItem container = (TreeViewItem)containerGenerator.ContainerFromItem(item);
            if (container != null)
                return container;

            foreach (object childItem in containerGenerator.Items)
            {
                TreeViewItem parent = containerGenerator.ContainerFromItem(childItem) as TreeViewItem;
                if (parent == null)
                    continue;

                container = parent.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (container != null)
                    return container;

                container = ContainerFromItem(parent.ItemContainerGenerator, item);
                if (container != null)
                    return container;
            }
            return null;
        }

        private TreeViewItem FindParentTreeViewItem(object child)
        {
            try
            {
                var parent = VisualTreeHelper.GetParent(child as DependencyObject);
                while ((parent as TreeViewItem) == null && parent != null)
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
                return parent as TreeViewItem;
            }
            catch
            {
                return null;
            }
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SelectedItem = e.NewValue;
            var tree = (TreeView)e.OriginalSource;

            var tvi = ContainerFromItem(tree.ItemContainerGenerator, tree.SelectedItem);
            var ptvi = FindParentTreeViewItem(tvi);
            if (ptvi != null)
                ParentItem = ptvi.DataContext;
            else
                ParentItem = null;
        }
    }
}
