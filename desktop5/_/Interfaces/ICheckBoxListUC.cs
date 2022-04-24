using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

public interface ICheckBoxListUC
{
    ListBox lb { get; }
        int Count { get; }
        bool? DialogResult { set; }
        NotifyChangesCollection<NotifyPropertyChangedWrapper<CheckBox>> l { get; set; }
        string Title { get; }

        event VoidBoolNullable ChangeDialogResult;
        event Action<object, ListOperation, object> CollectionChanged;

        void Accept(object input);
        void AddCheckbox(NotifyPropertyChangedWrapper<CheckBox> n);
        List<StackPanel> AllContent();
        Dictionary<StackPanel, bool> AllContentDict();
        List<string> AllContentString();
        void AttachChangeDialogResult(VoidBoolNullable a, bool throwException = true);
        IEnumerable<StackPanel> CheckedContent();
        IEnumerable<int> CheckedIndexes();
        List<string> CheckedStrings();
        void Clear();
        void ColButtons_Added(string s);
        int CountOfHandlersChangeDialogResult();
        void DefaultButtonsInit();
        void EventOn(EventOnArgs e);
        void FocusOnMainElement();
        bool HandleKey(KeyEventArgs e);
        void HideAllButtons();
        void Init();
        void Init(ImageButtonsInit i, IList<string> list = null, EventOnArgs e = null, bool defChecked = false);
        void InitializeComponent();
        void OnSizeChanged(DesktopSize s);
        void uc_Loaded(object sender, RoutedEventArgs e);
    object Tag { get; set; }
    }
