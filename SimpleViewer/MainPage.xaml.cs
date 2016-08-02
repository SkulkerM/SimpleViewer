using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SimpleViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
      public MainPage()
      {
        this.InitializeComponent();
      } // End of constructor

      /// <summary>
      /// Event handle for the canvas control draw event.  If we have page 
      /// data loaded, it draws the page.  Otherwise, it draws the banner 
      /// prompting, no, begging for some user attention.
      /// </summary>
      /// <param name="sender">Framework sending object</param>
      /// <param name="args">Event arguments</param>
      private void canvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
      {
        // erase the surface
        args.DrawingSession.Clear(Colors.Beige);
        // if we have a DOM, draw it
        if (m_pg != null)
          m_pg.Draw(args.DrawingSession, Matrix3x2.Identity);
        // otherwise, just print out a friendly message
        else
        {
          Microsoft.Graphics.Canvas.Text.CanvasTextFormat fmt = new Microsoft.Graphics.Canvas.Text.CanvasTextFormat()
          {
            FontSize = 24,
            FontFamily = "Arial"
          };
          args.DrawingSession.DrawText("Click anywhere to open a file...", new Vector2(100, 100), Colors.Black, fmt);
        } // End of else - page data
        return;
      } // End of method - canvasControl_Draw

      /// <summary>
      /// Event handler for a mouse button press on the canvas control.
      /// </summary>
      /// <param name="sender">Framework sending object</param>
      /// <param name="e">Event arguments</param>
      private async void canvasControl_press(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
      {
        // ignore additional presses until we finish (i.e. grab the 'inPress' lock)
        if (Interlocked.CompareExchange(ref m_inPress, 1, 0) == 1) return;
        // ask the user for a file
        var picker = new Windows.Storage.Pickers.FileOpenPicker();
        picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
        picker.SuggestedStartLocation =
            Windows.Storage.Pickers.PickerLocationId.Desktop;
        picker.FileTypeFilter.Add(".xml");
        Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
        if (file != null)
        {
          // remember the current cursor
          var savedCursor = Window.Current.CoreWindow.PointerCursor;
          // and then set it to the wait cursor
          Window.Current.CoreWindow.PointerCursor =
              new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Wait, 1); 
          try
          {
            // always knock out the page we're holding
            m_pg = null;
            // open the file to process it
            using (var stm = await file.OpenStreamForReadAsync())
            {
              // load the PDL into a DOM
              XmlSerializer s = new XmlSerializer(typeof(SimplePDL.Page));
              var pg = (SimplePDL.Page)s.Deserialize(stm);
              // cause the page to parse out values
              pg.Parse();
              // store the completely parsed page, ready for drawing
              m_pg = pg;
            } // End of using - file stream
          } // End of try block
          catch (Exception)
          {
            // restore the cursor
            Window.Current.CoreWindow.PointerCursor = savedCursor;
            // and tell 'em there be trouble
            var mb = new MessageDialog("I couldn't parse the contents of " + file.Name + ". Are you sure it is a SimplePDL?", 
                                       "Load failure");
            await mb.ShowAsync();
          } // End of catch block

          // (always) invalidate the draw surface
          drawCanvas.Invalidate();
          // return the cursor
          Window.Current.CoreWindow.PointerCursor = savedCursor;
        } // End of if - got the file from picker

        // let the next press in
        m_inPress = 0;
        return;
      } // End of canvasControl_press

    // Fields
    private SimplePDL.Page m_pg = null;  // the loaded page data
    int m_inPress = 0;                   // are we loading a file?
  } // End of class - MainPage
} // End of namespace - SimpleViewer
