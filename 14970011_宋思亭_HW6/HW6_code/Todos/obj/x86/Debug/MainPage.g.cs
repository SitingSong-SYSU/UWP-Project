﻿#pragma checksum "D:\15软工\现操\Pj 06\14970011_宋思亭_HW6_v1\Todos\Todos\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "76A8AC2F2CE291B9A47382B7CD48E673"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Todos
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(global::Windows.UI.Xaml.Controls.ItemsControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.ItemsSource = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_Primitives_ToggleButton_IsChecked(global::Windows.UI.Xaml.Controls.Primitives.ToggleButton obj, global::System.Nullable<global::System.Boolean> value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Nullable<global::System.Boolean>) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Nullable<global::System.Boolean>), targetNullValue);
                }
                obj.IsChecked = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_Image_Source(global::Windows.UI.Xaml.Controls.Image obj, global::Windows.UI.Xaml.Media.ImageSource value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::Windows.UI.Xaml.Media.ImageSource) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Xaml.Media.ImageSource), targetNullValue);
                }
                obj.Source = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Text(global::Windows.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        private class MainPage_obj20_Bindings :
            global::Windows.UI.Xaml.IDataTemplateExtension,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IMainPage_Bindings
        {
            private global::Todos.Models.TodoItem dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.CheckBox obj21;
            private global::Windows.UI.Xaml.Controls.Image obj22;
            private global::Windows.UI.Xaml.Controls.TextBlock obj23;

            private MainPage_obj20_BindingsTracking bindingsTracking;

            public MainPage_obj20_Bindings()
            {
                this.bindingsTracking = new MainPage_obj20_BindingsTracking(this);
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 21:
                        this.obj21 = (global::Windows.UI.Xaml.Controls.CheckBox)target;
                        (this.obj21).RegisterPropertyChangedCallback(global::Windows.UI.Xaml.Controls.Primitives.ToggleButton.IsCheckedProperty,
                            (global::Windows.UI.Xaml.DependencyObject sender, global::Windows.UI.Xaml.DependencyProperty prop) =>
                            {
                                if (this.initialized)
                                {
                                    // Update Two Way binding
                                    this.dataRoot.finish = this.obj21.IsChecked;
                                }
                            });
                        break;
                    case 22:
                        this.obj22 = (global::Windows.UI.Xaml.Controls.Image)target;
                        (this.obj22).RegisterPropertyChangedCallback(global::Windows.UI.Xaml.Controls.Image.SourceProperty,
                            (global::Windows.UI.Xaml.DependencyObject sender, global::Windows.UI.Xaml.DependencyProperty prop) =>
                            {
                                if (this.initialized)
                                {
                                    // Update Two Way binding
                                    this.dataRoot.img = this.obj22.Source;
                                }
                            });
                        break;
                    case 23:
                        this.obj23 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        (this.obj23).RegisterPropertyChangedCallback(global::Windows.UI.Xaml.Controls.TextBlock.TextProperty,
                            (global::Windows.UI.Xaml.DependencyObject sender, global::Windows.UI.Xaml.DependencyProperty prop) =>
                            {
                                if (this.initialized)
                                {
                                    // Update Two Way binding
                                    this.dataRoot.title = this.obj23.Text;
                                }
                            });
                        break;
                    default:
                        break;
                }
            }

            public void DataContextChangedHandler(global::Windows.UI.Xaml.FrameworkElement sender, global::Windows.UI.Xaml.DataContextChangedEventArgs args)
            {
                 if (this.SetDataRoot(args.NewValue))
                 {
                    this.Update();
                 }
            }

            // IDataTemplateExtension

            public bool ProcessBinding(uint phase)
            {
                throw new global::System.NotImplementedException();
            }

            public int ProcessBindings(global::Windows.UI.Xaml.Controls.ContainerContentChangingEventArgs args)
            {
                int nextPhase = -1;
                switch(args.Phase)
                {
                    case 0:
                        nextPhase = -1;
                        this.SetDataRoot(args.Item);
                        if (!removedDataContextHandler)
                        {
                            removedDataContextHandler = true;
                            ((global::Windows.UI.Xaml.Controls.UserControl)args.ItemContainer.ContentTemplateRoot).DataContextChanged -= this.DataContextChangedHandler;
                        }
                        this.initialized = true;
                        break;
                }
                this.Update_((global::Todos.Models.TodoItem) args.Item, 1 << (int)args.Phase);
                return nextPhase;
            }

            public void ResetTemplate()
            {
                this.bindingsTracking.ReleaseAllListeners();
            }

            // IMainPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::Todos.Models.TodoItem)newDataRoot;
                    return true;
                }
                return false;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::Todos.Models.TodoItem obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_finish(obj.finish, phase);
                        this.Update_img(obj.img, phase);
                        this.Update_title(obj.title, phase);
                    }
                }
            }
            private void Update_finish(global::System.Nullable<global::System.Boolean> obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_Primitives_ToggleButton_IsChecked(this.obj21, obj, null);
                }
            }
            private void Update_img(global::Windows.UI.Xaml.Media.ImageSource obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_Image_Source(this.obj22, obj, null);
                }
            }
            private void Update_title(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj23, obj, null);
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
            private class MainPage_obj20_BindingsTracking
            {
                private global::System.WeakReference<MainPage_obj20_Bindings> WeakRefToBindingObj; 

                public MainPage_obj20_BindingsTracking(MainPage_obj20_Bindings obj)
                {
                    WeakRefToBindingObj = new global::System.WeakReference<MainPage_obj20_Bindings>(obj);
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners_(null);
                }

                public void PropertyChanged_(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    MainPage_obj20_Bindings bindings;
                    if (WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        string propName = e.PropertyName;
                        global::Todos.Models.TodoItem obj = sender as global::Todos.Models.TodoItem;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                            if (obj != null)
                            {
                                bindings.Update_finish(obj.finish, DATA_CHANGED);
                                bindings.Update_img(obj.img, DATA_CHANGED);
                                bindings.Update_title(obj.title, DATA_CHANGED);
                            }
                        }
                        else
                        {
                            switch (propName)
                            {
                                case "finish":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_finish(obj.finish, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "img":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_img(obj.img, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "title":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_title(obj.title, DATA_CHANGED);
                                    }
                                    break;
                                }
                                default:
                                    break;
                            }
                        }
                    }
                }
                public void UpdateChildListeners_(global::Todos.Models.TodoItem obj)
                {
                    MainPage_obj20_Bindings bindings;
                    if (WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        if (bindings.dataRoot != null)
                        {
                            ((global::System.ComponentModel.INotifyPropertyChanged)bindings.dataRoot).PropertyChanged -= PropertyChanged_;
                        }
                        if (obj != null)
                        {
                            bindings.dataRoot = obj;
                            ((global::System.ComponentModel.INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged_;
                        }
                    }
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        private class MainPage_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IMainPage_Bindings
        {
            private global::Todos.MainPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ListView obj10;

            public MainPage_obj1_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 10:
                        this.obj10 = (global::Windows.UI.Xaml.Controls.ListView)target;
                        break;
                    default:
                        break;
                }
            }

            // IMainPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::Todos.MainPage)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::Todos.MainPage obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_ViewModel(obj.ViewModel, phase);
                    }
                }
            }
            private void Update_ViewModel(global::Todos.ViewModels.TodoItemViewModel obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_ViewModel_getItems(obj.getItems, phase);
                    }
                }
            }
            private void Update_ViewModel_getItems(global::System.Collections.ObjectModel.ObservableCollection<global::Todos.Models.TodoItem> obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj10, obj, null);
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2:
                {
                    this.All = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3:
                {
                    this.VisualStateGroup = (global::Windows.UI.Xaml.VisualStateGroup)(target);
                }
                break;
            case 4:
                {
                    this.VisualStateMin0 = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 5:
                {
                    this.VisualStateMin800 = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 6:
                {
                    this.textBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 7:
                {
                    this.updateTile = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 42 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.updateTile).Click += this.updatetile_Click;
                    #line default
                }
                break;
            case 8:
                {
                    this.search = (global::Windows.UI.Xaml.Controls.AutoSuggestBox)(target);
                    #line 43 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)this.search).QuerySubmitted += this.search_QuerySubmitted;
                    #line default
                }
                break;
            case 9:
                {
                    this.background = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 10:
                {
                    this.ToDoListView = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 49 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.ToDoListView).ItemClick += this.itemClick;
                    #line default
                }
                break;
            case 11:
                {
                    this.InlineToDoItemViewGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 12:
                {
                    this.pic = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 13:
                {
                    this.slider = (global::Windows.UI.Xaml.Controls.Slider)(target);
                }
                break;
            case 14:
                {
                    this.title = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 15:
                {
                    this.details = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 16:
                {
                    this.date = (global::Windows.UI.Xaml.Controls.DatePicker)(target);
                }
                break;
            case 17:
                {
                    this.createButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 117 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.createButton).Click += this.CreatButton_Click;
                    #line default
                }
                break;
            case 18:
                {
                    this.CancelButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 118 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.CancelButton).Click += this.CancelButton_Click;
                    #line default
                }
                break;
            case 19:
                {
                    this.SelectPictureButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 110 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.SelectPictureButton).Click += this.SelectPictureButton_Click;
                    #line default
                }
                break;
            case 21:
                {
                    global::Windows.UI.Xaml.Controls.CheckBox element21 = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                    #line 82 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.CheckBox)element21).Checked += this.checkBox;
                    #line 82 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.CheckBox)element21).Unchecked += this.uncheckBox;
                    #line default
                }
                break;
            case 24:
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element24 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    #line 91 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element24).Click += this.share_Click;
                    #line default
                }
                break;
            case 25:
                {
                    this.delete = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 129 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.delete).Click += this.Delete_Click;
                    #line default
                }
                break;
            case 26:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element26 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 130 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element26).Click += this.NewPage_Click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    MainPage_obj1_Bindings bindings = new MainPage_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            case 20:
                {
                    global::Windows.UI.Xaml.Controls.UserControl element20 = (global::Windows.UI.Xaml.Controls.UserControl)target;
                    MainPage_obj20_Bindings bindings = new MainPage_obj20_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(element20.DataContext);
                    element20.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Windows.UI.Xaml.DataTemplate.SetExtensionInstance(element20, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

