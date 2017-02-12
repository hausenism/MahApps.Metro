﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MahApps.Metro.Controls
{
    public sealed class ItemClickEventArgs : RoutedEventArgs
    {
        public ItemClickEventArgs(object clickedObject)
        {
            this.ClickedItem = clickedObject;
        }

        public object ClickedItem { get; private set; }
    }

    public delegate void ItemClickEventHandler(object sender, ItemClickEventArgs e);

    /// <summary>
    /// The HamburgerMenu is based on a SplitView control. By default it contains a HamburgerButton and a ListView to display menu items.
    /// </summary>
    public partial class HamburgerMenu
    {
        /// <summary>
        /// Event raised when an item is clicked
        /// </summary>
        public event ItemClickEventHandler ItemClick;

        /// <summary>
        /// Event raised when an options' item is clicked
        /// </summary>
        public event ItemClickEventHandler OptionsItemClick;

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            IsPaneOpen = !IsPaneOpen;
        }

        private void OnItemClick()
        {
            if (_optionsListView != null)
            {
                _optionsListView.SelectedIndex = -1;
            }

            ItemClick?.Invoke(this, new ItemClickEventArgs(_buttonsListView.SelectedItem));
        }

        private void OnOptionsItemClick()
        {
            if (_buttonsListView != null)
            {
                _buttonsListView.SelectedIndex = -1;
            }

            OptionsItemClick?.Invoke(this, new ItemClickEventArgs(_optionsListView.SelectedItem));
        }

        private void ButtonsListView_ItemClick(object sender, MouseButtonEventArgs e)
        {
            OnItemClick();
        }

        private void OptionsListView_ItemClick(object sender, MouseButtonEventArgs e)
        {
            OnOptionsItemClick();
        }

        private void ButtonsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                if (Keyboard.IsKeyToggled(Key.Space) ||
                    Keyboard.IsKeyToggled(Key.Up) ||
                    Keyboard.IsKeyToggled(Key.PageUp) ||
                    Keyboard.IsKeyToggled(Key.Down) ||
                    Keyboard.IsKeyToggled(Key.PageDown))
                {
                    OnItemClick();
                }
            }
        }

        private void OptionsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                if (Keyboard.IsKeyToggled(Key.Space) ||
                    Keyboard.IsKeyToggled(Key.Up) ||
                    Keyboard.IsKeyToggled(Key.PageUp) ||
                    Keyboard.IsKeyToggled(Key.Down) ||
                    Keyboard.IsKeyToggled(Key.PageDown))
                {
                    OnOptionsItemClick();
                }
            }
        }
    }
}