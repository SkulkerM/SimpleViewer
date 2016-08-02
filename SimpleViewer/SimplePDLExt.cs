using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Windows.UI;
using System.Globalization;
using System.Numerics;
using Windows.Foundation;

namespace SimplePDL
{
  /// <summary>
  /// Interface for common extensions for the classes created by XSD
  /// </summary>
  public interface NodeExtension
  {
    void Draw(CanvasDrawingSession ds, Matrix3x2 mtx);
    void Parse();
  } // End of interface - NodeExtension

  /// <summary>
  /// Class holding parsing utilities 
  /// </summary>
  class ParseUtils
  {
    /// <summary>
    /// Parses a string of comma separated reals into an array of doubles
    /// </summary>
    /// <param name="str">The string to parse</param>
    /// <param name="cnt">The number of reals expected</param>
    /// <returns>An array of 'cnt' doubles</returns>
    static public Double[] ParseDoubles(string str, int cnt)
    {
      // split into comma delimited values
      string[] s = str.Split(',');
      // sanity check the count we're expecting against what we have
      if (cnt != s.Length)
        throw new Exception("Invalid value count");
      // grab a new array of the appropriate size
      Double[] db = new Double[s.Length];
      // and parse the individual values
      for (int i = 0; i < s.Length; i++) db[i] = Double.Parse(s[i]);
      return db;
    } // End of method - ParseDoubles

    /// <summary>
    /// Parses a '#RRGGBB' or '#ARRGGBB' string into a color value
    /// </summary>
    /// <param name="str">The string to parse.</param>
    /// <returns>The parsed color value. If the string is 'null', a 
    /// fully transparent color is returned.</returns>
    static public Color ParseColor(string str)
    {
      // if no string, just pass back transparency
      if (str == null) return Colors.Transparent;
      // sanity check the length
      if (str.Length != 9 && str.Length != 7)
        throw new Exception("Invalid color format; clr: " + str);
      // if we have alpha
      if (str.Length == 9)
        return Color.FromArgb(byte.Parse(str.Substring(1, 2), NumberStyles.HexNumber),
                              byte.Parse(str.Substring(3, 2), NumberStyles.HexNumber),
                              byte.Parse(str.Substring(5, 2), NumberStyles.HexNumber),
                              byte.Parse(str.Substring(7, 2), NumberStyles.HexNumber));
      // otherwise, opaque
      return Color.FromArgb(0xFF,
                            byte.Parse(str.Substring(1, 2), NumberStyles.HexNumber),
                            byte.Parse(str.Substring(3, 2), NumberStyles.HexNumber),
                            byte.Parse(str.Substring(5, 2), NumberStyles.HexNumber));
    } // End of method - ParseColor

    /// <summary>
    /// Parses a 3x2 matrix from a string, formatted as a sequence of comma 
    /// delimited reals in M11,M12,M21,M22,M31,M32 order
    /// </summary>
    /// <param name="str">The string to parse.</param>
    /// <returns>The matrix parsed.  If the string parameter is 'null', 
    /// the identity matrix is returned.</returns>
    static public Matrix3x2 ParseMatrix(string str)
    {
      Matrix3x2 mtx;
      if (str == null) mtx = Matrix3x2.Identity;
      else
      {
        Double[] m = ParseDoubles(str, 6);
        mtx.M11 = (float)m[0]; mtx.M12 = (float)m[1];
        mtx.M21 = (float)m[2]; mtx.M22 = (float)m[3];
        mtx.M31 = (float)m[4]; mtx.M32 = (float)m[5];
      } // End of else - have a string
      return mtx;
    } // End of method - ParseMatrix

    /// <summary>
    /// Parses a rectangle, represented as left,top,right,bottom real values, 
    /// from a string
    /// </summary>
    /// <param name="str">The string to parse.</param>
    /// <returns>The parsed rectangle. If the string parameter is 'null', an
    /// empty rectangle is returned.</returns>
    static public Rect ParseRectangle(string str)
    {
      Rect rct;
      if (str == null) rct = Rect.Empty;
      else
      {
        Double[] r = ParseDoubles(str, 4);
        rct = new Rect(r[0], r[1], r[2], r[3]);
      }
      return rct;
    } // End of method - ParseRectangle

    /// <summary>
    /// Parses a vector, represented as x,y reals, from a string
    /// </summary>
    /// <param name="str">The string to parse</param>
    /// <returns>The parsed vector.  If the string parameter is 'null', a 
    /// zero vector is returned.</returns>
    static public Vector2 ParseVector(string str)
    {
      Vector2 vc;
      if (str == null) vc = Vector2.Zero;
      else
      {
        Double[] v = ParseDoubles(str, 2);
        vc = new Vector2((float)v[0], (float)v[1]);
      }
      return vc;
    } // End of method - ParseVector
  } // End of class - ParseUtils
  
  /// <summary>
  /// Class representing a Page element.  Note: These element classes are 
  /// partial classes that add to those defined by the XSD generated code.
  /// </summary>
  public partial class Page : NodeExtension
  {
    /// <summary>
    /// Draws the page content to the drawing session
    /// </summary>
    /// <param name="ds">Current drawing session</param>
    /// <param name="mtx">Transformation matrix from our 'parent'</param>
    public void Draw(CanvasDrawingSession ds, Matrix3x2 mtx)
    {
      ds.Transform = mtx;
      // erase the dimensions
      ds.FillRectangle(0.0f, 0.0f, (float)m_dims[0], (float)m_dims[1], Colors.White);
      // run through children, drawing each
      foreach (var chd  in Items)
      {
        (chd as NodeExtension).Draw(ds, mtx);
      }
      return;
    } // End of method - Draw

    /// <summary>
    /// Performs an secondary parsing needed for the class.
    /// </summary>
    public void Parse()
    {
      // parse our dimensions
      m_dims = ParseUtils.ParseDoubles(Dimensions, 2);
      // run through children, asking them to parse too
      foreach (var chd in Items)
      {
        (chd as NodeExtension).Parse();
      }
      return;
    } // End of method - Parse

    // Fields
    private Double[] m_dims = null;
  } // End of class - Page

  /// <summary>
  /// Class to represent a Circle element
  /// </summary>
  public partial class ctCircle : NodeExtension
  {
    /// <summary>
    /// Draws a circle on the drawing session
    /// </summary>
    /// <param name="ds">The current drawing session</param>
    /// <param name="mtx">Parent's effective transform</param>
    public void Draw(CanvasDrawingSession ds, Matrix3x2 mtx)
    {
      // create a layer to draw with our opacity (Note: combines with other 
      // layers above us, like a canvas opacity)
      using (ds.CreateLayer((float)Opacity))
      {
        // combine our transformation with the one from our parent
        ds.Transform = mtx * m_mtx;
        // fill the circle - if we have a fill
        if (Fill != null) ds.FillCircle(m_center, (float)Radius, m_fill);
        // outline the circle - if we have a stroke
        if (Stroke != null && StrokeWidth > 0)
          ds.DrawCircle(m_center, (float)Radius, m_stroke, (float)StrokeWidth);
      } // End of using - opacity layer
      return;
    } // End of method - draw

    /// <summary>
    /// Performs an secondary parsing needed for the class.
    /// </summary>
    public void Parse()
    {
      if (!OpacitySpecified) Opacity = 1.0;
      m_mtx = ParseUtils.ParseMatrix(Transform);
      m_center = ParseUtils.ParseVector(Center);
      m_fill = ParseUtils.ParseColor(Fill);
      m_stroke = ParseUtils.ParseColor(Stroke);
      return;
    } // End of method - Parse

    // Fields
    private Matrix3x2 m_mtx;
    private Vector2 m_center;
    private Color m_fill;
    private Color m_stroke;
  } // End of class - ctCircle

  /// <summary>
  /// Class representing a Text element
  /// </summary>
  public partial class ctText : NodeExtension
  {
    /// <summary>
    /// Draws text to the drawing session
    /// </summary>
    /// <param name="ds">The current drawing session</param>
    /// <param name="mtx">Parent's effective transform</param>
    public void Draw(CanvasDrawingSession ds, Matrix3x2 mtx)
    {
      using (ds.CreateLayer((float)Opacity))
      {
        Microsoft.Graphics.Canvas.Text.CanvasTextFormat fmt = new Microsoft.Graphics.Canvas.Text.CanvasTextFormat()
        {
          FontSize = (float)Size,
          FontFamily = Font
        };
        ds.Transform = mtx * m_mtx;
        ds.DrawText(String, m_org, m_clr, fmt);
      } // End of using - opacity layer
      return;
    } // End of method - draw

    /// <summary>
    /// Performs an secondary parsing needed for the class.
    /// </summary>
    public void Parse()
    {
      if (!OpacitySpecified) Opacity = 1.0;
      m_mtx = ParseUtils.ParseMatrix(Transform);
      m_org = ParseUtils.ParseVector(Origin);
      m_clr = ParseUtils.ParseColor(Color);
      return;
    } // End of method - Parse

    // Fields
    private Matrix3x2 m_mtx;
    private Vector2 m_org;
    private Color m_clr;
  } // End of class - ctText

  /// <summary>
  /// Class representing a Line element
  /// </summary>
  public partial class ctLine : NodeExtension
  {
    /// <summary>
    /// Draws a line to the drawing session
    /// </summary>
    /// <param name="ds">The current drawing session</param>
    /// <param name="mtx">Parent's effective transform</param>
    public void Draw(CanvasDrawingSession ds, Matrix3x2 mtx)
    {
      using (ds.CreateLayer((float)Opacity))
      {
        ds.Transform = mtx * m_mtx;
        ds.DrawLine((float)m_pts[0], (float)m_pts[1], (float)m_pts[2], 
                    (float)m_pts[3], m_clr, (float)Width);
      } // End of using - opacity layer
      return;
    } // End of method - Draw

    /// <summary>
    /// Performs an secondary parsing needed for the class.
    /// </summary>
    public void Parse()
    {
      if (!OpacitySpecified) Opacity = 1.0;
      m_mtx = ParseUtils.ParseMatrix(Transform);
      m_pts = ParseUtils.ParseDoubles(Points, 4);
      m_clr = ParseUtils.ParseColor(Color);
      return;
    } // End of method - Parse

    // Fields
    private Matrix3x2 m_mtx = Matrix3x2.Identity;
    private Double[] m_pts;
    private Color m_clr;
  } // End of class - ctLine

  /// <summary>
  /// Class representing an Rectangle element
  /// </summary>
  public partial class ctRectangle : NodeExtension
  {
    /// <summary>
    /// Draws the rectangle to the drawing session
    /// </summary>
    /// <param name="ds">The current drawing session</param>
    /// <param name="mtx">Parent's effective transform</param>
    public void Draw(CanvasDrawingSession ds, Matrix3x2 mtx)
    {
      using (ds.CreateLayer((float)Opacity))
      {
        ds.Transform = mtx * m_mtx;
        if (Fill != null) ds.FillRectangle(m_rect, m_fill);
        if (Stroke != null && StrokeWidth > 0)
          ds.DrawRectangle(m_rect, m_stroke, (float)StrokeWidth);
      } // End of using - opacity layer
      return;
    } // End of method - Draw

    /// <summary>
    /// Performs an secondary parsing needed for the class.
    /// </summary>
    public void Parse()
    {
      if (!OpacitySpecified) Opacity = 1.0;
      m_mtx = ParseUtils.ParseMatrix(Transform);
      m_rect = ParseUtils.ParseRectangle(Points);
      m_fill = ParseUtils.ParseColor(Fill);
      m_stroke = ParseUtils.ParseColor(Stroke);
      return;
    } // End of method - Parse

    // Fields
    private Matrix3x2 m_mtx = Matrix3x2.Identity;
    private Rect m_rect;                         
    private Color m_fill;
    private Color m_stroke;
  } // End of class - ctRectangle

  /// <summary>
  /// Class representing a Canvas element
  /// </summary>
  public partial class ctCanvas : NodeExtension
  {
    /// <summary>
    /// Draws the canvas and its children to the drawing session
    /// </summary>
    /// <param name="ds">Current drawing session</param>
    /// <param name="mtx">Parent's effective transformation</param>
    public void Draw(CanvasDrawingSession ds, Matrix3x2 mtx)
    {
      using (ds.CreateLayer((float)Opacity))
      {
        // combine parent's transform with our own
        Matrix3x2 lmtx = mtx * m_mtx;
        // run through children, drawing each
        foreach (var chd in Items)
        {
          (chd as NodeExtension).Draw(ds, lmtx);
        }
      } // End of using - opacity layer
      return;
    } // End of method - Draw

    /// <summary>
    /// Performs an secondary parsing needed for the class.
    /// </summary>
    public void Parse()
    {
      if (!OpacitySpecified) Opacity = 1.0;
      m_mtx = ParseUtils.ParseMatrix(Transform);
      // run through children, parsing each
      foreach (var chd in Items)
      {
        (chd as NodeExtension).Parse();
      }
      return;
    } // End of method - Parse
    // Fields
    private Matrix3x2 m_mtx = Matrix3x2.Identity;
  } // End of class - ctCanvas
} // End of namespace - SimplePDL
