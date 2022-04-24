using desktop.Controls;
using desktop.Controls.Collections;
using desktop.Controls.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IMainWindowBasicControls
{
    ICheckBoxListUC checkBoxListUC
    {
        get; set;
    }

    
    SelectOneValue selectOneValue
    {
        get; set;
    }
    RadioButtonsList radioButtonsList
    {
        get; set;
    }
    EnterOneValueUC enterOneValueUC
    {
        get; set;
    }
    ShowTextResult showTextResult
    {
        get; set;
    }
    WindowWithUserControl windowWithUserControl
    {
        get; set;
    }
    //
    
    ShowTextResultWindow showTextResultWindow
    {
        get; set;
    }

    ICompareInCheckBoxListUC compareInCheckBoxListUC
    {
        get; set;
    }

    SelectTwoValues selectTwoValues
    {
        get; set;
    }

    #region Static
    //static ICheckBoxListUC checkBoxListUC { get => mw.checkBoxListUC; set => mw.checkBoxListUC = value; }
    //static SelectOneValue selectOneValue { get => mw.selectOneValue; set => mw.selectOneValue = value; }
    //static RadioButtonsList radioButtonsList { get => mw.radioButtonsList; set => mw.radioButtonsList = value; }
    //static EnterOneValueUC enterOneValueUC { get => mw.enterOneValueUC; set => mw.enterOneValueUC = value; }
    //static ShowTextResult showTextResult { get => mw.showTextResult; set => mw.showTextResult = value; }
    //static WindowWithUserControl windowWithUserControl { get => mw.mw; set => mw.mw = value; }
    //static ShowTextResultWindow showTextResultWindow { get => mw.showTextResultWindow; set => mw.showTextResultWindow = value; } 
    //static CompareInCheckBoxListUC compareInCheckBoxListUC { get => mw.compareInCheckBoxListUC; set => mw.compareInCheckBoxListUC = value; }
    //static SelectTwoValues selectTwoValues { get => mw.selectTwoValues; set => mw.selectTwoValues = value; }
    #endregion

    #region Non-Static
    //ICheckBoxListUC checkBoxListUC { get => mw.checkBoxListUC; set => mw.checkBoxListUC = value; }
    //SelectOneValue selectOneValue { get => mw.selectOneValue; set => mw.selectOneValue = value; }
    //RadioButtonsList radioButtonsList { get => mw.radioButtonsList; set => mw.radioButtonsList = value; }
    //EnterOneValueUC enterOneValueUC { get => mw.enterOneValueUC; set => mw.enterOneValueUC = value; }
    //ShowTextResult showTextResult { get => mw.showTextResult; set => mw.showTextResult = value; }
    //WindowWithUserControl windowWithUserControl { get => mw.mw; set => mw.mw = value; }
    //ShowTextResultWindow showTextResultWindow { get => mw.showTextResultWindow; set => mw.showTextResultWindow = value; }
    //CompareInCheckBoxListUC compareInCheckBoxListUC { get => mw.compareInCheckBoxListUC; set => mw.compareInCheckBoxListUC = value; }
    //SelectTwoValues selectTwoValues { get => mw.selectTwoValues; set => mw.selectTwoValues = value; }
    #endregion
}