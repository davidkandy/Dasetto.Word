using Dasetto.Word.AttachedProperties;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Dasetto.Word
{


    /// <summary>
    /// The IsBusy attached property for anything that wants to flag if the control is busy
    /// </summary>
    public class IsBusyProperty : BaseAttachedProperty<HasTextProperty, bool> 
    {

    };
}
