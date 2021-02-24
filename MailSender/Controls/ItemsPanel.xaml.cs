using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace MailSender.Controls
{
    public partial class ItemsPanel
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(ItemsPanel),
                new PropertyMetadata("(Название)"));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        #region AddNewItemCommand : ICommand - Добавление нового элемента

        /// <summary>Добавление нового элемента</summary>
        public static readonly DependencyProperty AddNewItemCommandProperty =
            DependencyProperty.Register(
                nameof(AddNewItemCommand),
                typeof(ICommand),
                typeof(ItemsPanel),
                new PropertyMetadata(default(ICommand)));

        /// <summary>Добавление нового элемента</summary>
        //[Category("")]
        [Description("Добавление нового элемента")]
        public ICommand AddNewItemCommand
        {
            get => (ICommand) GetValue(AddNewItemCommandProperty); 
            set => SetValue(AddNewItemCommandProperty, value);
        }

        #endregion

        #region EditItemCommand : ICommand - Редактирование элемента

        /// <summary>Редактирование элемента</summary>
        public static readonly DependencyProperty EditItemCommandProperty =
            DependencyProperty.Register(
                nameof(EditItemCommand),
                typeof(ICommand),
                typeof(ItemsPanel),
                new PropertyMetadata(default(ICommand)));

        /// <summary>Редактирование элемента</summary>
        //[Category("")]
        [Description("Редактирование элемента")]
        public ICommand EditItemCommand
        {
            get => (ICommand)GetValue(EditItemCommandProperty);
            set => SetValue(EditItemCommandProperty, value);
        }

        #endregion

        #region RemoveItemCommand : ICommand - Удаление элемента

        /// <summary>Удаление элемента</summary>
        public static readonly DependencyProperty RemoveItemCommandProperty =
            DependencyProperty.Register(
                nameof(RemoveItemCommand),
                typeof(ICommand),
                typeof(ItemsPanel),
                new PropertyMetadata(default(ICommand)));

        /// <summary>Удаление элемента</summary>
        //[Category("")]
        [Description("Удаление элемента")]
        public ICommand RemoveItemCommand
        {
            get => (ICommand)GetValue(RemoveItemCommandProperty);
            set => SetValue(RemoveItemCommandProperty, value);
        }

        #endregion

        #region ItemSource : IEnumerable - Элементы панели

        /// <summary>Элементы панели</summary>
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register(
                nameof(ItemSource),
                typeof(IEnumerable),
                typeof(ItemsPanel),
                new PropertyMetadata(default(IEnumerable)));

        /// <summary>Элементы панели</summary>
        //[Category("")]
        [Description("Элементы панели")]
        public IEnumerable ItemSource
        {
            get => (IEnumerable) GetValue(ItemSourceProperty); 
            set => SetValue(ItemSourceProperty, value);
        }

        #endregion

        #region SelectedItem : object - Выбранный элемент

        /// <summary>Выбранный элемент</summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                nameof(SelectedItem),
                typeof(object),
                typeof(ItemsPanel),
                new PropertyMetadata(default(object)));

        /// <summary>Выбранный элемент</summary>
        //[Category("")]
        [Description("Выбранный элемент")]
        public object SelectedItem { get => (object) GetValue(SelectedItemProperty); set => SetValue(SelectedItemProperty, value); }

        #endregion

        #region ItemTemplate : DataTemplate - Шаблон элемента выпадающего списка

        /// <summary>Шаблон элемента выпадающего списка</summary>
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(
                nameof(ItemTemplate),
                typeof(DataTemplate),
                typeof(ItemsPanel),
                new PropertyMetadata(default(DataTemplate)));

        /// <summary>Шаблон элемента выпадающего списка</summary>
        //[Category("")]
        [Description("Шаблон элемента выпадающего списка")]
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate) GetValue(ItemTemplateProperty); 
            set => SetValue(ItemTemplateProperty, value);
        }

        #endregion

        #region DisplayMemberPath : string - Имя отображаемого свойства

        /// <summary>Имя отображаемого свойства</summary>
        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register(
                nameof(DisplayMemberPath),
                typeof(string),
                typeof(ItemsPanel),
                new PropertyMetadata(default(string)));

        /// <summary>Имя отображаемого свойства</summary>
        //[Category("")]
        [Description("Имя отображаемого свойства")]
        public string DisplayMemberPath { get => (string) GetValue(DisplayMemberPathProperty); set => SetValue(DisplayMemberPathProperty, value); }

        #endregion

        public ItemsPanel() => InitializeComponent();
    }
}
