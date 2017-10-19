#region NameSpaces

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Windows;

#endregion

/* Theme Name : Delight Theme
* Author : Narwin
* Date : 1/5/2017
* Credits : 
* AeonHacks( RoundRectangle Function ) ,
* Xerts( Some ScrollBar & ListBox Events) ,
*/

#region Helper

public sealed class HelperMethods
{

    #region MouseStates

    /// <summary>
    /// The helper enumerator to get mouse states.
    /// </summary>
    public enum MouseMode : byte
    {
        Normal,
        Hovered,
        Pushed,
        Disabled
    }

    #endregion

    #region Draw Methods

    /// <summary>
    /// The Method to draw the image from encoded base64 string.
    /// </summary>
    /// <param name="G">The Graphics to draw the image.</param>
    /// <param name="Base64Image">The Encoded base64 image.</param>
    /// <param name="Rect">The Rectangle area for the image.</param>
    public void DrawImageFromBase64(Graphics G, string Base64Image, Rectangle Rect)
    {
        Image IM = null;
        using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Convert.FromBase64String(Base64Image)))
        {
            IM = Image.FromStream(ms);
            ms.Close();
        }
        G.DrawImage(IM, Rect);
    }

    /// <summary>
    /// The Method to fill rounded rectangle.
    /// </summary>
    /// <param name="G">The Graphics to draw the image.</param>
    /// <param name="C">The Color to the rectangle area.</param>
    /// <param name="Rect">The Rectangle area to be filled.</param>
    /// <param name="Curve">The Rounding border radius.</param>
    /// <param name="TopLeft">Wether the top left of rectangle be round or not.</param>
    /// <param name="TopRight">Wether the top right of rectangle be round or not.</param>
    /// <param name="BottomLeft">Wether the bottom left of rectangle be round or not.</param>
    /// <param name="BottomRight">Wether the bottom right of rectangle be round or not.</param>
    public void FillRoundedPath(Graphics G, Color C, Rectangle Rect, int Curve, bool TopLeft = true,
        bool TopRight = true, bool BottomLeft = true, bool BottomRight = true)
    {
        G.FillPath(new SolidBrush(C), RoundRec(Rect, Curve, TopLeft, TopRight, BottomLeft, BottomRight));
    }

    /// <summary>
    /// The Method to fill the rounded rectangle.
    /// </summary>
    /// <param name="G">The Graphics to fill the rectangle.</param>
    /// <param name="B">The brush to the rectangle area.</param>
    /// <param name="Rect">The Rectangle area to be filled.</param>
    /// <param name="Curve">The Rounding border radius.</param>
    /// <param name="TopLeft">Wether the top left of rectangle be round or not.</param>
    /// <param name="TopRight">Wether the top right of rectangle be round or not.</param>
    /// <param name="BottomLeft">Wether the bottom left of rectangle be round or not.</param>
    /// <param name="BottomRight">Wether the bottom right of rectangle be round or not.</param>
    public void FillRoundedPath(Graphics G, Brush B, Rectangle Rect, int Curve, bool TopLeft = true,
        bool TopRight = true, bool BottomLeft = true, bool BottomRight = true)
    {
        G.FillPath(B, RoundRec(Rect, Curve, TopLeft, TopRight, BottomLeft, BottomRight));
    }

    /// <summary>
    /// The Method to fill the rectangle the base color and surrounding with another color(Rectangle with shadow).
    /// </summary>
    /// <param name="G">The Graphics to fill the rectangle.</param>
    /// <param name="CenterColor">The Center color of the rectangle area.</param>
    /// <param name="SurroundColor">The Inner Surround color of the rectangle area.</param>
    /// <param name="P">The Point of the surrounding color.</param>
    /// <param name="Rect">The Rectangle area to be filled.</param>
    /// <param name="Curve">The Rounding border radius.</param>
    /// <param name="TopLeft">Wether the top left of rectangle be round or not.</param>
    /// <param name="TopRight">Wether the top right of rectangle be round or not.</param>
    /// <param name="BottomLeft">Wether the bottom left of rectangle be round or not.</param>
    /// <param name="BottomRight">Wether the bottom right of rectangle be round or not.</param>
    public void FillWithInnerRectangle(Graphics G, Color CenterColor, Color SurroundColor, Point P, Rectangle Rect,
        int Curve, bool TopLeft = true, bool TopRight = true, bool BottomLeft = true, bool BottomRight = true)
    {
        using (
            PathGradientBrush PGB =
                new PathGradientBrush(RoundRec(Rect, Curve, TopLeft, TopRight, BottomLeft, BottomRight)))
        {

            PGB.CenterColor = CenterColor;
            PGB.SurroundColors = new Color[] { SurroundColor };
            PGB.FocusScales = P;
            GraphicsPath GP = new GraphicsPath { FillMode = FillMode.Winding };
            GP.AddRectangle(Rect);
            G.FillPath(PGB, GP);
            GP.Dispose();
        }
    }

    /// <summary>
    /// The Method to fill the circle the base color and surrounding with another color(Rectangle with shadow).
    /// </summary>
    /// <param name="G">The Graphics to fill the circle.</param>
    /// <param name="CenterColor">The Center color of the rectangle area.</param>
    /// <param name="SurroundColor">The Inner Surround color of the rectangle area.</param>
    /// <param name="P">The Point of the surrounding color.</param>
    /// <param name="Rect">The circle area to be filled.</param>
    public void FillWithInnerEllipse(Graphics G, Color CenterColor, Color SurroundColor, Point P, Rectangle Rect)
    {
        GraphicsPath GP = new GraphicsPath { FillMode = FillMode.Winding };
        GP.AddEllipse(Rect);
        using (PathGradientBrush PGB = new PathGradientBrush(GP))
        {
            PGB.CenterColor = CenterColor;
            PGB.SurroundColors = new Color[] { SurroundColor };
            PGB.FocusScales = P;
            G.FillPath(PGB, GP);
            GP.Dispose();
        }
    }

    /// <summary>
    /// The Method to fill the rounded rectangle the base color and surrounding with another color(Rectangle with shadow).
    /// </summary>
    /// <param name="G">The Graphics to fill rounded the rectangle.</param>
    /// <param name="CenterColor">The Center color of the rectangle area.</param>
    /// <param name="SurroundColor">The Inner Surround color of the rectangle area.</param>
    /// <param name="P">The Point of the surrounding color.</param>
    /// <param name="Rect">The Rectangle area to be filled.</param>
    /// <param name="Curve">The Rounding border radius.</param>
    /// <param name="TopLeft">Wether the top left of rectangle be round or not.</param>
    /// <param name="TopRight">Wether the top right of rectangle be round or not.</param>
    /// <param name="BottomLeft">Wether the bottom left of rectangle be round or not.</param>
    /// <param name="BottomRight">Wether the bottom right of rectangle be round or not.</param>
    public void FillWithInnerRoundedPath(Graphics G, Color CenterColor, Color SurroundColor, Point P, Rectangle Rect,
        int Curve, bool TopLeft = true, bool TopRight = true, bool BottomLeft = true, bool BottomRight = true)
    {
        using (
            PathGradientBrush PGB =
                new PathGradientBrush(RoundRec(Rect, Curve, TopLeft, TopRight, BottomLeft, BottomRight)))
        {
            PGB.CenterColor = CenterColor;
            PGB.SurroundColors = new Color[] { SurroundColor };
            PGB.FocusScales = P;
            G.FillPath(PGB, RoundRec(Rect, Curve, TopLeft, TopRight, BottomLeft, BottomRight));
        }
    }

    /// <summary>
    /// The Method to draw the rounded rectangle area.
    /// </summary>
    /// <param name="G">The Graphics to draw rounded the rectangle.</param>
    /// <param name="C">Border Color</param>
    /// <param name="Size">Border thickness</param>
    /// <param name="Rect">The Rectangle area to be drawn.</param>
    /// <param name="Curve">The Rounding border radius.</param>
    /// <param name="TopLeft">Wether the top left of rectangle be round or not.</param>
    /// <param name="TopRight">Wether the top right of rectangle be round or not.</param>
    /// <param name="BottomLeft">Wether the bottom left of rectangle be round or not.</param>
    /// <param name="BottomRight">Wether the bottom right of rectangle be round or not.</param>
    public void DrawRoundedPath(Graphics G, Color C, float Size, Rectangle Rect, int Curve, bool TopLeft = true,
        bool TopRight = true, bool BottomLeft = true, bool BottomRight = true)
    {
        G.DrawPath(new Pen(C, Size), RoundRec(Rect, Curve, TopLeft, TopRight, BottomLeft, BottomRight));
    }

    /// <summary>
    /// The method to draw the triangle.
    /// </summary>
    /// <param name="G">The Graphics to draw triangle.</param>
    /// <param name="C">The Triangle Color.</param>
    /// <param name="Size">The Triangle thickness</param>
    /// <param name="P1">Point 1</param>
    /// <param name="P2">Point 2</param>
    /// <param name="P3">Point 3</param>
    /// <param name="P4">Point 4</param>
    /// <param name="P5">Point 5</param>
    /// <param name="P6">Point 6</param>
    public void DrawTriangle(Graphics G, Color C, int Size, Point P1_0, Point P1_1, Point P2_0, Point P2_1, Point P3_0,
        Point P3_1)
    {
        G.DrawLine(new Pen(C, Size), P1_0, P1_1);
        G.DrawLine(new Pen(C, Size), P2_0, P2_1);
        G.DrawLine(new Pen(C, Size), P3_0, P3_1);
    }

    /// <summary>
    /// The Method to fill the rectangle with border.
    /// </summary>
    /// <param name="G">The Graphics to fill the the rectangle.</param>
    /// <param name="Rect">The Rectangle to fill.</param>
    /// <param name="RectColor">The Rectangle color.</param>
    /// <param name="StrokeColor">The Stroke(Border) color.</param>
    /// <param name="StrokeSize">The Stroke thickness.</param>
    public void FillStrokedRectangle(Graphics G, Rectangle Rect, Color RectColor, Color Stroke, int StrokeSize = 1)
    {
        using (SolidBrush B = new SolidBrush(RectColor))
        using (Pen S = new Pen(Stroke, StrokeSize))
        {
            G.FillRectangle(B, Rect);
            G.DrawRectangle(S, Rect);
        }

    }

    /// <summary>
    /// The Method to fill rounded rectangle with border.
    /// </summary>
    /// <param name="G">The Graphics to fill rounded the rectangle.</param>
    /// <param name="Rect">The Rectangle to fill.</param>
    /// <param name="RectColor">The Rectangle color.</param>
    /// <param name="StrokeColor">The Stroke(Border) color.</param>
    /// <param name="StrokeSize">The Stroke thickness.</param>
    /// <param name="Curve">The Rounding border radius.</param>
    /// <param name="TopLeft">Wether the top left of rectangle be round or not.</param>
    /// <param name="TopRight">Wether the top right of rectangle be round or not.</param>
    /// <param name="BottomLeft">Wether the bottom left of rectangle be round or not.</param>
    /// <param name="BottomRight">Wether the bottom right of rectangle be round or not.</param>
    public void FillRoundedStrokedRectangle(Graphics G, Rectangle Rect, Color RectColor, Color Stroke,
        int StrokeSize = 1, int curve = 1, bool TopLeft = true, bool TopRight = true, bool BottomLeft = true,
        bool BottomRight = true)
    {
        using (SolidBrush B = new SolidBrush(RectColor))
        {
            using (Pen S = new Pen(Stroke, StrokeSize))
            {
                FillRoundedPath(G, B, Rect, curve, TopLeft, TopRight, BottomLeft, BottomRight);
                DrawRoundedPath(G, Stroke, StrokeSize, Rect, curve, TopLeft, TopRight, BottomLeft, BottomRight);
            }
        }
    }

    /// <summary>
    /// The Method to draw the image with custom color.
    /// </summary>
    /// <param name="G"> The Graphic to draw the image.</param>
    /// <param name="R"> The Rectangle area of image.</param>
    /// <param name="_Image"> The image that the custom color applies on it.</param>
    /// <param name="C">The Color that be applied to the image.</param>
    /// <remarks></remarks>
    public void DrawImageWithColor(Graphics G, Rectangle R, Image _Image, Color C)
    {
        float[][] ptsArray = new float[][]
        {
            new float[] {Convert.ToSingle(C.R / 255.0), 0f, 0f, 0f, 0f},
            new float[] {0f, Convert.ToSingle(C.G / 255.0), 0f, 0f, 0f},
            new float[] {0f, 0f, Convert.ToSingle(C.B / 255.0), 0f, 0f},
            new float[] {0f, 0f, 0f, Convert.ToSingle(C.A / 255.0), 0f},
            new float[]
            {
                Convert.ToSingle( C.R/255.0),
                Convert.ToSingle( C.G/255.0),
                Convert.ToSingle( C.B/255.0), 0f,
                Convert.ToSingle( C.A/255.0)
            }
        };
        ImageAttributes imgAttribs = new ImageAttributes();
        imgAttribs.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Default);
        G.DrawImage(_Image, R, 0, 0, _Image.Width, _Image.Height, GraphicsUnit.Pixel, imgAttribs);
        _Image.Dispose();
    }


    /// <summary>
    /// The Method to draw the image with custom color.
    /// </summary>
    /// <param name="G"> The Graphic to draw the image.</param>
    /// <param name="R"> The Rectangle area of image.</param>
    /// <param name="_Image"> The Encoded base64 image that the custom color applies on it.</param>
    /// <param name="C">The Color that be applied to the image.</param>
    /// <remarks></remarks>
    public void DrawImageWithColor(Graphics G, Rectangle R, string _Image, Color C)
    {
        Image IM = ImageFromBase64(_Image);
        float[][] ptsArray = new float[][]
        {
            new float[] {Convert.ToSingle(C.R / 255.0), 0f, 0f, 0f, 0f},
            new float[] {0f, Convert.ToSingle(C.G / 255.0), 0f, 0f, 0f},
            new float[] {0f, 0f, Convert.ToSingle(C.B / 255.0), 0f, 0f},
            new float[] {0f, 0f, 0f, Convert.ToSingle(C.A / 255.0), 0f},
            new float[]
            {
                Convert.ToSingle( C.R/255.0),
                Convert.ToSingle( C.G/255.0),
                Convert.ToSingle( C.B/255.0), 0f,
                Convert.ToSingle( C.A/255.0)
            }
        };
        ImageAttributes imgAttribs = new ImageAttributes();
        imgAttribs.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Default);
        G.DrawImage(IM, R, 0, 0, IM.Width, IM.Height, GraphicsUnit.Pixel, imgAttribs);
    }

    #endregion

    #region Shapes

    /// <summary>
    /// The Triangle that joins 3 points to the triangle shape.
    /// </summary>
    /// <param name="P1">Point 1.</param>
    /// <param name="P2">Point 2.</param>
    /// <param name="P3">Point 3.</param>
    /// <returns>The Trangle shape based on given points.</returns>
    public Point[] Triangle(Point P1, Point P2, Point P3)
    {
        return new Point[] {
            P1,
            P2,
            P3
        };
    }

    #endregion

    #region Brushes

    /// <summary>
    /// The Brush with two colors one center another surounding the center based on the given rectangle area. 
    /// </summary>
    /// <param name="CenterColor">The Center color of the rectangle.</param>
    /// <param name="SurroundColor">The Surrounding color of the rectangle.</param>
    /// <param name="P">The Point of surrounding.</param>
    /// <param name="Rect">The Rectangle of the brush.</param>
    /// <returns>The Brush with two colors one center another surounding the center.</returns>
    public static PathGradientBrush GlowBrush(Color CenterColor, Color SurroundColor, Point P, Rectangle Rect)
    {
        GraphicsPath GP = new GraphicsPath { FillMode = FillMode.Winding };
        GP.AddRectangle(Rect);
        return new PathGradientBrush(GP)
        {
            CenterColor = CenterColor,
            SurroundColors = new Color[] { SurroundColor },
            FocusScales = P
        };
    }

    /// <summary>
    /// The Brush from RGBA color.
    /// </summary>
    /// <param name="R">Red.</param>
    /// <param name="G">Green.</param>
    /// <param name="B">Blue.</param>
    /// <param name="A">Alpha.</param>
    /// <returns>The Brush from given RGBA color.</returns>
    public SolidBrush SolidBrushRGBColor(int R, int G, int B, int A = 0)
    {
        return new SolidBrush(Color.FromArgb(A, R, G, B));
    }

    /// <summary>
    /// The Brush from HEX color.
    /// </summary>
    /// <param name="C_WithoutHash">HEX Color without hash.</param>
    /// <returns>The Brush from given HEX color.</returns>
    public SolidBrush SolidBrushHTMlColor(string C_WithoutHash)
    {
        return new SolidBrush(GetHTMLColor(C_WithoutHash));
    }

    #endregion

    #region Pens

    /// <summary>
    /// The Pen from RGBA color.
    /// </summary>
    /// <param name="R">Red.</param>
    /// <param name="G">Green.</param>
    /// <param name="B">Blue.</param>
    /// <param name="A">Alpha.</param>
    /// <returns>The Pen from given RGBA color.</returns>
    public Pen PenRGBColor(int R, int G, int B, int A, float Size)
    {
        return new Pen(Color.FromArgb(A, R, G, B), Size);
    }

    /// <summary>
    /// The Pen from HEX color.
    /// </summary>
    /// <param name="C_WithoutHash">HEX Color without hash.</param>
    /// <param name="Size">The Size of the pen.</param>
    /// <returns></returns>
    public Pen PenHTMlColor(string C_WithoutHash, float Size = 1)
    {
        return new Pen(GetHTMLColor(C_WithoutHash), Size);
    }

    #endregion

    #region Colors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="C_WithoutHash"></param>
    /// <returns></returns>
    public Color GetHTMLColor(string C_WithoutHash)
    {
        return ColorTranslator.FromHtml("#" + C_WithoutHash);
    }

    /// <summary>
    /// The Color from HEX by alpha property.
    /// </summary>
    /// <param name="alpha">Alpha.</param>
    /// <param name="C_WithoutHash">HEX Color without hash.</param>
    /// <returns>The Color from HEX with given ammount of transparency</returns>
    public Color GetAlphaHTMLColor(int alpha, string C_WithoutHash)
    {
        return Color.FromArgb(alpha, ColorTranslator.FromHtml("#" + C_WithoutHash));
    }

    #endregion

    #region Methods

    /// <summary>
    /// The String format to provide the alignment.
    /// </summary>
    /// <param name="Horizontal">Horizontal alignment.</param>
    /// <param name="Vertical">Horizontal alignment. alignment.</param>
    /// <returns>The String format.</returns>
    public StringFormat SetPosition(StringAlignment Horizontal = StringAlignment.Center, StringAlignment Vertical = StringAlignment.Center)
    {
        return new StringFormat
        {
            Alignment = Horizontal,
            LineAlignment = Vertical
        };
    }

    /// <summary>
    /// The Matrix array of single from color.
    /// </summary>
    /// <param name="C">The Color.</param>
    /// <returns>The Matrix array of single from the given color</returns>
    public float[][] ColorToMatrix(Color C)
    {
        return new float[][] {
            new float[] {
                Convert.ToSingle(C.R / 255),
                0,
                0,
                0,
                0
            },
            new float[] {
                0,
                Convert.ToSingle(C.G / 255),
                0,
                0,
                0
            },
            new float[] {
                0,
                0,
                Convert.ToSingle(C.B / 255),
                0,
                0
            },
            new float[] {
                0,
                0,
                0,
                Convert.ToSingle(C.A / 255),
                0
            },
            new float[] {
                Convert.ToSingle(C.R / 255),
                Convert.ToSingle(C.G / 255),
                Convert.ToSingle(C.B / 255),
                0f,
                Convert.ToSingle(C.A / 255)
            }
        };
    }

    /// <summary>
    /// The Image from encoded base64 image.
    /// </summary>
    /// <param name="Base64Image">The Encoded base64 image</param>
    /// <returns>The Image from encoded base64.</returns>
    public Image ImageFromBase64(string Base64Image)
    {
        using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Convert.FromBase64String(Base64Image)))
        {
            return Image.FromStream(ms);
        }
    }

    /// <summary>
    /// The Method the rotate the given image.
    /// </summary>
    /// <param name="img">The Image to rotate.</param>
    /// <param name="angle">The Rotation angle.</param>
    /// <param name="matrixorder">The Order for matrix operation.</param>
    /// <returns>The Rotated image.</returns>
    public Image RotateImage(Image img, float angle, MatrixOrder matrixorder = MatrixOrder.Append)
    {
        Bitmap b = new Bitmap(img.Width, img.Height, img.PixelFormat);
        b.SetResolution(img.HorizontalResolution, img.VerticalResolution);
        using (Graphics g = Graphics.FromImage(b))
        {
            Matrix m = new Matrix();
            m.RotateAt(angle, new PointF(Convert.ToSingle(img.Width) / 2, Convert.ToSingle(img.Height) / 2), matrixorder);
            g.Transform = m;
            m.Dispose();
            g.DrawImage(img, new Point(0, 0));
            img.Dispose();
        }
        return b;
    }

    #endregion

    #region Round Border

    /// <summary>
    /// Credits : AeonHack
    /// </summary>

    public GraphicsPath RoundRec(Rectangle r, int Curve, bool TopLeft = true, bool TopRight = true,
      bool BottomLeft = true, bool BottomRight = true)
    {
        GraphicsPath CreateRoundPath = new GraphicsPath(FillMode.Winding);
        if (TopLeft)
        {
            CreateRoundPath.AddArc(r.X, r.Y, Curve, Curve, 180f, 90f);
        }
        else
        {
            CreateRoundPath.AddLine(r.X, r.Y, r.X, r.Y);
        }
        if (TopRight)
        {
            CreateRoundPath.AddArc(r.Right - Curve, r.Y, Curve, Curve, 270f, 90f);
        }
        else
        {
            CreateRoundPath.AddLine(r.Right - r.Width, r.Y, r.Width, r.Y);
        }
        if (BottomRight)
        {
            CreateRoundPath.AddArc(r.Right - Curve, r.Bottom - Curve, Curve, Curve, 0f, 90f);
        }
        else
        {
            CreateRoundPath.AddLine(r.Right, r.Bottom, r.Right, r.Bottom);

        }
        if (BottomLeft)
        {
            CreateRoundPath.AddArc(r.X, r.Bottom - Curve, Curve, Curve, 90f, 90f);
        }
        else
        {
            CreateRoundPath.AddLine(r.X, r.Bottom, r.X, r.Bottom);
        }
        CreateRoundPath.CloseFigure();
        return CreateRoundPath;
    }

    #endregion

}

#endregion

#region TabControl

#region  TabControl 

public class DelightTabControl : TabControl
{

    #region  Declarations 

    private static readonly HelperMethods H = new HelperMethods();

    private Point _ImageBackArea,
        _ImageLocation,
        _TextLocation,
        _HeaderTextLocation,
        _LocatedPosition,
        _ArrowsLocation;
    private Color _TabColor, 
        _TabPageColor, 
        _TabBackgroundColor, 
        _TabSelectedTextColor, 
        _TabUnSelectedTextColor,
        _SmallRectColor,
        _TabTextHeaderColor, 
        _TabLinesColor, 
        _ArrowsColor, 
        _SelectedTabSmallRectColor;
    private bool _UseAnimation;
    private string NextImage;
		
	#endregion

	#region  Constructors 

	public DelightTabControl()
    {
        SetStyle(ControlStyles.UserPaint | 
            ControlStyles.OptimizedDoubleBuffer | 
            ControlStyles.AllPaintingInWmPaint | 
            ControlStyles.SupportsTransparentBackColor,
            true);
        DoubleBuffered = true;
        SizeMode = TabSizeMode.Fixed;
        Dock = DockStyle.None;
        ItemSize = new Size(40, 210);
        Alignment = TabAlignment.Left;
        Font = new Font("Segoe UI", 9);
        UpdateStyles();
        SelectedIndex = 1;
        _ImageBackArea = new Point(30, 7);
        _ImageLocation = new Point(35, 11);
        _TextLocation = new Point(60, 10);
        _HeaderTextLocation = new Point(25, 5);
        _TabColor = Colors.TabColor;
        _TabPageColor = Colors.White;
        _TabBackgroundColor = Colors.TabClear;
        _TabSelectedTextColor = Colors.TabSelected;
        _TabUnSelectedTextColor = Colors.TabUnSelected;
        _TabTextHeaderColor = Colors.TabHeader;
        _TabLinesColor = H.GetHTMLColor("FF202020");
        _ArrowsColor = Colors.TabUnSelected;
        _SelectedTabSmallRectColor = Colors.White;
        _SmallRectColor = Colors.TabColor2;
        _UseAnimation = true;
        _LocatedPosition = new Point(-1, -1);
        _ArrowsLocation = new Point(25, 15);
        NextImage = "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAABHdJREFUeNrsWztMIlEUBUQlonEqjZ8IhTHrN6DNlrtaaGy0MbtasYl2FhorO1tNcBZt7BRNDFRoJxVb0phQWJhYrDHG0ImaTPyz94xvktldwDEwwDzmJjdPBWXuuWfuO++A1nQ6banksFkqPEwATABMAEwATABMAEwAKjfser+A1Wq1NDc3T9bU1Ij0rdtmkzE/f3p6Wry6ujrI9+/nq2R1ZwCKpyWC4lU/xteR1tZWX6kZYNX7LNDR0fEbBTudzjfK2d9I9/z8LK+SJH29uLj4xS0D/ul8pogQSB5uGeB2u+UXaGhokL+nWfDX44+Pj1hSt7e3XmLCOXcMaGxsTGl4msCYIHC3Dc7NzUVxv7++vsqJXUGdtbW1chJDPJQxl8slcAXA/Px8fHp6Oqrx6ZgFIlczgOIT5XeKiePjY48yC5ge+C8eHh6w7JycnPzgYgYQzU9pOQqFQodDQ0MJjb/mo3mwwI0UJhDitCTW19ejNBST6DI6l6l7qpkg9vb2+rgAgIFwQMovvr+/H2xqakpq/LVtGopfeDoMHbW0tJz7/f4wAXLPNEDGcDgcchITIn19fR4uAEDRGHCDg4Ona2trQaL6vUaNEKOZ4OaBAQoIoeHh4fOlpaUw5kGumQDlSCnU19dHenp6BMMDwECAOtwhfXDq8/kOP6ARYu3t7YLhAWAgYBCGiAWJ8fHxKOZBppmgKEbGBA/NBZELAFhxOPwcrK6uxr1er2aN0NnZuW0YJYjuaVBzn2kZm5qamjg7O/OofYMsStGSSqUWLy8vfxrBD9AslAKBQPQDGkGkeTBp6Fsgk1AKUgiCkBWEqqoqOaurq5EiNwCohRIxIaxRI7i5AkARSpIkJXOd/tSZb9jLjAGW7u5uBy3fnE6no0Buk3EAgBtE1I8x0ZMxXl5e5BUu0+zsbJS3GSDmKl4dcJngNnHDADrxQdz4su3/SuehA2Cs0GEKxR9xwQDm/mgyP1A83CUUz9wmYzOAOo/CRbhA2aY+AucEiCS/34/7PsHEk7F3Aeo83B5Nuh7Fw00inRCHaCrUNdhL2HkMu0i2zqu1P0QRXCSIpELc9yWfAczdiTG3J2egeLhHcJEgkphYshiWAczVicDlea/ziOXl5fDIyAg6Hyp08UVnAHNzYlr3erhFMzMzSudTelxTURkANweuTi6fQOk8XCK4RazzSb2uqWgMYC6Opr0e7hBcIrhFzDXSLYrCgLa2tgUaZj7lPJ+r811dXYnd3V1F6CT0vjZbEYqftGh8xxd7/ebmZkGFTskZQNpeVN/z/57hlc7X1dUl9/b2gnCFCil0ymEGvOvaYK/f2toKU/EFFzolZ4DSceVTYRnO9VB5ugmdkjPgPddmZWUlPDo6qpvQKTkAcG1wn0uSJOfd3Z2cNzc3MDV0FzqaKKpzLgQCgVB/f/81nQHSLpcrPTAwcL2xsRGix1Yo3aW8/qJ9RijLYwf57vX5Xn9R3hqj1wAIY6rTX6pQjk7ZA1DuYf7DhAmACYAJgAmACUAFxx8BBgA3wk1zjSsvoAAAAABJRU5ErkJggg==";
}

    protected sealed override bool DoubleBuffered
    {
        get { return base.DoubleBuffered; }
        set { base.DoubleBuffered = value; }
    }

    #endregion
    
    #region  Properties 

[Category("Custom Properties"), Description("Gets or sets the tabcontrol ItemSize.")]
    public Size ChangeSize
    {
        get { return ItemSize; }
        set
        {
            ItemSize = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabcontrol left side color.")]
    public Color TabColor
    {
        get { return _TabColor; }
        set
        {
            _TabColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabcontrol background color.")]
    public Color TabBackgroundColor
    {
        get { return _TabBackgroundColor; }
        set
        {
            _TabBackgroundColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabpages color of the tabcontrol.")]
    public Color TabPageColor
    {
        get { return _TabPageColor; }
        set
        {
            _TabPageColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabcontrol small rectangle color while of the images.")]
    public Color SmallRectColor
    {
        get { return _SmallRectColor; }
        set
        {
            _SmallRectColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabpages arrow color of the tabcontrol.")]
    public Color ArrowsColor
    {
        get { return _ArrowsColor; }
        set
        {
            _ArrowsColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabpage Text color while selected.")]
    public Color TabSelectedTextColor
    {
        get { return _TabSelectedTextColor; }
        set
        {
            _TabSelectedTextColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabpage header Text color while Taged.")]
    public Color TabTextHeaderColor
    {
        get { return _TabTextHeaderColor; }
        set
        {
            _TabTextHeaderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabpage Text color while un-selected.")]
    public Color TabUnSelectedTextColor
    {
        get { return _TabUnSelectedTextColor; }
        set
        {
            _TabUnSelectedTextColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabcontrol selected lines color while selected.")]
    public Color TabLinesColor
    {
        get { return _TabLinesColor; }
        set
        {
            _TabLinesColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabcontrol small rectangle color while selected.")]
    public Color SelectedTabSmallRectColor
    {
        get { return _SelectedTabSmallRectColor; }
        set
        {
            _SelectedTabSmallRectColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets whether the tabcontrol use animation to sliding or not.")]
    public bool UseAnimation
    {
        get { return _UseAnimation; }
        set
        {
            _UseAnimation = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tab pages image location.")]
    public Point ImageLocation
    {
        get { return _ImageLocation; }
        set
        {
            _ImageLocation = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tab pages text location.")]
    public Point TextLocation
    {
        get { return _TextLocation; }
        set
        {
            _TextLocation = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tab pages header text location.")]
    public Point HeaderTextLocation
    {
        get { return _HeaderTextLocation; }
        set
        {
            _HeaderTextLocation = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the rectangles location behind the image.")]
    public Point ImageBackArea
    {
        get { return _ImageBackArea; }
        set
        {
            _ImageBackArea = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tab pages arrows location.")]
    public Point ArrowsLocation
    {
        get { return _ArrowsLocation; }
        set
        {
            _ArrowsLocation = value;
            Invalidate();
        }
    }


    #endregion

    #region  Draw Control 


    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;

        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        G.InterpolationMode = InterpolationMode.HighQualityBicubic;
        G.CompositingQuality = CompositingQuality.HighQuality;

        using (SolidBrush TC = new SolidBrush(TabColor))
        using (SolidBrush TPC = new SolidBrush(TabBackgroundColor))
        {
            G.FillRectangle(TC, new Rectangle(0, 0, ItemSize.Height, Height));
            G.FillRectangle(TPC, new Rectangle(ItemSize.Height, 0, Width, Height));
        }

        for (int i = 0; i <= TabCount - 1; i++)
        {
            Rectangle R = GetTabRect(i);
            Cursor = Cursors.Hand;
            using (SolidBrush B = new SolidBrush(SmallRectColor))
            {
                if (TabPages[i].Tag == null)
                {
                    G.FillRectangle(B, new Rectangle(R.X + ImageBackArea.X, R.Y + ImageBackArea.Y, 24, 24));
                    H.DrawImageWithColor(G, new Rectangle(R.Width - ArrowsLocation.X, R.Y + ArrowsLocation.Y, 10, 10), NextImage, ArrowsColor);
                }
            }

            if (TabPages[i].Tag != null)
            {
                G.DrawString(TabPages[i].Text.ToUpper(), Font, new SolidBrush(TabTextHeaderColor), new Rectangle(R.X + (TextLocation.X - HeaderTextLocation.X), R.Y + (TextLocation.Y + HeaderTextLocation.Y), R.Width, R.Height));
                using (Pen P = new Pen(TabLinesColor))
                {
                    G.DrawLine(P, new Point(ItemSize.Height, 0), new Point(ItemSize.Height, Height));
                }
            }
            else if (SelectedIndex == i && TabPages[i].Tag == null)
            {
                using (SolidBrush TC = new SolidBrush(TabSelectedTextColor))
                {
                    G.DrawString(TabPages[i].Text, Font, TC, new Rectangle(R.X + TextLocation.X, R.Y + TextLocation.Y, R.Width, R.Height));
                }
                if ((ImageList != null) && (ImageList.Images[i] != null))
                {
                    H.DrawImageWithColor(G, new Rectangle(R.X + ImageLocation.X, R.Y + ImageLocation.Y, 14, 14), ImageList.Images[i], TabSelectedTextColor);
                }

            }
            else
            {
                if (R.Contains(_LocatedPosition))
                {
                    if ((ImageList != null) && (ImageList.Images[i] != null))
                    {
                        using (SolidBrush TC = new SolidBrush(TabSelectedTextColor))
                        {

                            H.DrawImageWithColor(G, new Rectangle(R.X + ImageLocation.X, R.Y + ImageLocation.Y, 14, 14), H.RotateImage(ImageList.Images[i], 15), TabSelectedTextColor);

                            G.DrawString(TabPages[i].Text, Font, TC, new Rectangle(R.X + TextLocation.X, R.Y + TextLocation.Y, R.Width, R.Height));

                        }
                    }
                }
                else
                {
                    if ((ImageList != null) && (ImageList.Images[i] != null))
                    {
                        H.DrawImageWithColor(G, new Rectangle(R.X + ImageLocation.X, R.Y + ImageLocation.Y, 14, 14), ImageList.Images[i], TabUnSelectedTextColor);
                    }
                    using (SolidBrush TBC = new SolidBrush(TabUnSelectedTextColor))
                    {
                        G.DrawString(TabPages[i].Text, Font, TBC, new Rectangle(R.X + TextLocation.X, R.Y + TextLocation.Y, R.Width, R.Height));
                    }
                }

            }

        }

    }


    #endregion

    #region  Events 

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        foreach (TabPage Tab in base.TabPages)
        {
            Tab.BackColor = TabPageColor;
        }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        _LocatedPosition = e.Location;
        Invalidate();
    }

    #endregion
    
}

#endregion

#region Horizontal TabControl 

public class DelightHorizontalTabControl : TabControl
{

    #region Declarations 

    private static readonly HelperMethods H = new HelperMethods();
    private Color _TabColor = Colors.TabClear;
    private Color _TabPageColor = Colors.White;
    private Color _TabSelectedTextColor = Colors.White;
    private Color _TabUnSelectedTextColor = Colors.TabUnSelected2;
    private Color _TabSelectedBackgroundColor = Colors.TabSelected2;
    private bool _ShowLine = true;

    #endregion

    #region Constructors 

    public DelightHorizontalTabControl()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        SizeMode = TabSizeMode.Fixed;
        Dock = DockStyle.None;
        ItemSize = new Size(80, 40);
        Font = new Font("Segoe UI", 8);
        Alignment = TabAlignment.Top;
        UpdateStyles();
    }

    protected sealed override bool DoubleBuffered
    {
        get { return base.DoubleBuffered; }
        set { base.DoubleBuffered = value; }
    }

    #endregion

    #region Draw Control 


    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;

        G.Clear(TabPageColor);

        Cursor = Cursors.Hand;

        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        if (ShowLine)
        {
            using (Pen P = new Pen(TabColor, 2))
            {
                G.DrawLine(P, 2, ItemSize.Height, Width - 3, ItemSize.Height);
            }
        }

        for (int i = 0; i <= TabCount - 1; i++)
        {
            Rectangle R = GetTabRect(i);

            if (SelectedIndex == i)
            {
                using (SolidBrush B = new SolidBrush(TabSelectedBackgroundColor))
                using (SolidBrush TB = new SolidBrush(TabSelectedTextColor))
                {
                    if (ShowLine)
                    {
                        G.SmoothingMode = SmoothingMode.AntiAlias;
                        H.FillRoundedPath(G, B, new Rectangle(R.X, R.Y + 1, R.Width, R.Height - 4), 3, true, true, false,
                            false);
                        G.SmoothingMode = SmoothingMode.Default;
                    }
                    else
                    {
                        G.FillRectangle(B, new Rectangle(R.X, R.Y + 1, R.Width, R.Height - 4));
                    }
                    G.DrawString(TabPages[i].Text, Font, TB, R, H.SetPosition());
                }

            }
            else
            {
                using (SolidBrush B = new SolidBrush(TabUnSelectedTextColor))
                {
                    G.DrawString(TabPages[i].Text, Font, B, R, H.SetPosition());
                }
            }

        }

    }

    #endregion

    #region Events 

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        for (int i = 0; i <= TabCount - 1; i++)
        {
            Rectangle R = GetTabRect(i);
            if (R.Contains(e.Location))
            {
                Cursor = Cursors.Hand;
                Invalidate();
            }
            else
            {
                Cursor = Cursors.Arrow;
                Invalidate();
            }
        }
    }

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        foreach (TabPage Tab in TabPages)
        {
            Tab.BackColor = TabPageColor;
            Invalidate();
        }
    }

    #endregion

    #region Properties 

    [Category("Custom Properties"), Description("Gets or sets wheter the line be shown.")]
    public bool ShowLine
    {
        get { return _ShowLine; }
        set
        {
            _ShowLine = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabpages color of the tabcontrol.")]
    public Color TabPageColor
    {
        get { return _TabPageColor; }
        set
        {
            _TabPageColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabcontrol header color.")]
    public Color TabColor
    {
        get { return _TabColor; }
        set
        {
            _TabColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabpage Text color while selected.")]
    public Color TabSelectedTextColor
    {
        get { return _TabSelectedTextColor; }
        set
        {
            _TabSelectedTextColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabpage backcolor while selected.")]
    public Color TabSelectedBackgroundColor
    {
        get { return _TabSelectedBackgroundColor; }
        set
        {
            _TabSelectedBackgroundColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabpage Text color while un-selected.")]
    public Color TabUnSelectedTextColor
    {
        get { return _TabUnSelectedTextColor; }
        set
        {
            _TabUnSelectedTextColor = value;
            Invalidate();
        }
    }

    #endregion

}

#endregion

#region Vertical Mini TabControl 

public class DelightMiniTabControl : TabControl
{

    #region Declarations 

    private static readonly HelperMethods H = new HelperMethods();
    private Color 
        _TabColor,
        _TabPageColor,
        _TabBackgroundColor,
        _SmallRectColor,
        _TabSelectedColor,
        _TabUnSelectedColor;
    private bool _UseAnimation;
    private Point _LocatedPosition;

    #endregion

    #region  Constructors 

    public DelightMiniTabControl()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        SizeMode = TabSizeMode.Fixed;
        Dock = DockStyle.None;
        Size = new Size(450, 250);
        base.ItemSize = new Size(40, 55);
        Alignment = TabAlignment.Left;
        Font = new Font("Segoe UI", 8);
        UpdateStyles();
        SelectedIndex = 1;
        _TabColor = Colors.TabColor;
        _TabPageColor = Colors.White;
        _TabSelectedColor = Colors.TabSelected;
        _TabUnSelectedColor = Colors.TabUnSelected;
        _UseAnimation = true;
        _LocatedPosition = new Point(-1, -1);
        _TabBackgroundColor = Colors.TabClear;
        _SmallRectColor = Colors.TabColor2;
    }

    #endregion

    #region  Properties 

    [Category("Custom Properties"), Description("Gets or sets the tabcontrol left side color")]
    public Color TabColor
    {
        get { return _TabColor; }
        set
        {
            _TabColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabcontrol small rectangle color while of the images.")]
    public Color SmallRectColor
    {
        get { return _SmallRectColor; }
        set
        {
            _SmallRectColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabpages color of the tabcontrol")]
    public Color TabPageColor
    {
        get { return _TabPageColor; }
        set
        {
            _TabPageColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabcontrol background color.")]
    public Color TabBackgroundColor
    {
        get { return _TabBackgroundColor; }
        set
        {
            _TabBackgroundColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabpage Text color while selected")]
    public Color TabSelectedColor
    {
        get { return _TabSelectedColor; }
        set
        {
            _TabSelectedColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the tabpage color while un-selected")]
    public Color TabUnSelectedColor
    {
        get { return _TabUnSelectedColor; }
        set
        {
            _TabUnSelectedColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets whether the tabcontrol use animation to sliding or not.")]
    public bool UseAnimation
    {
        get { return _UseAnimation; }
        set
        {
            _UseAnimation = value;
            Invalidate();
        }
    }

    [Browsable(false)]
    public new Size ItemSize
    {
        get { return new Size(50, 55); }
        set { base.ItemSize = new Size(50, 55); }
    }


    #endregion

    #region  Draw Control 


    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;

        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        G.InterpolationMode = InterpolationMode.HighQualityBicubic;

        using (SolidBrush TC = new SolidBrush(TabColor))
        using (SolidBrush TPC = new SolidBrush(TabBackgroundColor))
        {
            G.FillRectangle(TC, new Rectangle(0, 0, ItemSize.Height, Height));
            G.FillRectangle(TPC, new Rectangle(ItemSize.Height, 0, Width, Height));
        }

        for (int i = 0; i <= TabCount - 1; i++)
        {
            Rectangle R = GetTabRect(i);
            Cursor = Cursors.Hand;

            using (SolidBrush B = new SolidBrush(SmallRectColor))
            {
                G.FillRectangle(B, new Rectangle(R.X + 8, R.Y + 11, R.Width - 20, R.Height - 10));
            }

            if (SelectedIndex == i)
            {
                if ((ImageList != null) && (ImageList.Images[i] != null))
                {
                    H.DrawImageWithColor(G, new Rectangle(R.X + 16, R.Y + 17, 18, 18), ImageList.Images[i], TabSelectedColor);
                }
            }
            else
            {
                if ((ImageList != null) && (ImageList.Images[i] != null))
                {
                    if (R.Contains(_LocatedPosition))
                    {
                        H.DrawImageWithColor(G, new Rectangle(R.X + 16, R.Y + 17, 18, 18), H.RotateImage(ImageList.Images[i], 15), TabSelectedColor);
                    }
                    else
                    {
                        H.DrawImageWithColor(G, new Rectangle(R.X + 16, R.Y + 17, 18, 18), ImageList.Images[i], TabUnSelectedColor);
                    }
                }
            }

        }

    }

    #endregion

    #region  Events 

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        foreach (TabPage Tab in base.TabPages)
        {
            Tab.BackColor = TabPageColor;
        }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (!DesignMode)
            _LocatedPosition = e.Location;
        Invalidate();
    }

    #endregion

}

#endregion

#endregion

#region  Button 

public class DelightButton : Control
{

    #region  Declarations 

    private Color
        _NormalColor,
        _NormalBorderColor,
        _NormalTextColor,
        _HoverColor,
        _HoverBorderColor,
        _HoverTextColor,
        _PushedColor,
        _PushedBorderColor,
        _PushedTextColor;
    private int _RoundRadius;
    private HelperMethods.MouseMode State;
    private bool _IsEnabled;
    private static readonly HelperMethods H = new HelperMethods();

    #endregion

    #region  Constructors 

    public DelightButton()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        UpdateStyles();
        BackColor = Color.Transparent;
        Font = new Font("Segoe UI", 10);
        State = HelperMethods.MouseMode.Normal;
        _RoundRadius = 0;
        _IsEnabled = true;
        _NormalColor = Colors.ButtonBase;
        _NormalBorderColor = Colors.ButtonBorder;
        _NormalTextColor = Colors.ButtonText;
        _HoverColor = Colors.ButtonBaseHover;
        _HoverBorderColor = Colors.ButtonHoverBorder;
        _HoverTextColor = Colors.ButtonHoverText;
        _PushedColor = Colors.ButtonBasePushed;
        _PushedBorderColor = Colors.ButtonPushedBorder;
        _PushedTextColor = Colors.ButtonPushedText;
    }

    #endregion

    #region  Properties 

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether the control can Rounded in corners.")]
    public int RoundRadius
    {
        get { return _RoundRadius; }
        set
        {
            _RoundRadius = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether the control can respond to user interaction.")]
    public bool IsEnabled
    {
        get { return _IsEnabled; }
        set
        {
            Enabled = value;
            _IsEnabled = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the button color in normal mouse state.")]
    public Color NormalColor
    {
        get { return _NormalColor; }
        set
        {
            _NormalColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the button border color in normal mouse state.")]
    public Color NormalBorderColor
    {
        get { return _NormalBorderColor; }
        set
        {
            _NormalBorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the button Text color in normal mouse state.")]
    public Color NormalTextColor
    {
        get { return _NormalTextColor; }
        set
        {
            _NormalTextColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the button color in hover mouse state.")]
    public Color HoverColor
    {
        get { return _HoverColor; }
        set
        {
            _HoverColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the button border color in hover mouse state.")]
    public Color HoverBorderColor
    {
        get { return _HoverBorderColor; }
        set
        {
            _HoverBorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the button Text color in hover mouse state.")]
    public Color HoverTextColor
    {
        get { return _HoverTextColor; }
        set
        {
            _HoverTextColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the button color in mouse down state.")]
    public Color PushedColor
    {
        get { return _PushedColor; }
        set
        {
            _PushedColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the button border color in mouse down state.")]
    public Color PushedBorderColor
    {
        get { return _PushedBorderColor; }
        set
        {
            _PushedBorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the button Text color in mouse down state.")]
    public Color PushedTextColor
    {
        get { return _PushedTextColor; }
        set
        {
            _PushedTextColor = value;
            Invalidate();
        }
    }


    #endregion

    #region  Draw Control 

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;
        GraphicsPath GP = new GraphicsPath();
        Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);

        if (RoundRadius > 0)
        {
            G.SmoothingMode = SmoothingMode.AntiAlias;
            GP = H.RoundRec(Rect, RoundRadius);
        }
        else
        {
            GP.AddRectangle(Rect);
        }

        GP.CloseFigure();

        G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

        switch (State)
        {

            case HelperMethods.MouseMode.Normal:

                using (SolidBrush BG = new SolidBrush(NormalColor))
                using (Pen P = new Pen(NormalBorderColor))
                using (SolidBrush TB = new SolidBrush(NormalTextColor))
                {
                    G.FillPath(BG, GP);
                    G.DrawPath(P, GP);
                    G.DrawString(Text, Font, TB, new Rectangle(0, 0, Width, Height), H.SetPosition());
                }

                break;
            case HelperMethods.MouseMode.Hovered:

                Cursor = Cursors.Hand;
                using (SolidBrush BG = new SolidBrush(HoverColor))
                using (Pen P = new Pen(HoverBorderColor))
                using (SolidBrush TB = new SolidBrush(HoverTextColor))
                {
                    G.FillPath(BG, GP);
                    G.DrawPath(P, GP);
                    G.DrawString(Text, Font, TB, new Rectangle(0, 0, Width, Height), H.SetPosition());
                }

                break;
            case HelperMethods.MouseMode.Pushed:

                using (SolidBrush BG = new SolidBrush(PushedColor))
                using (Pen P = new Pen(PushedBorderColor))
                using (SolidBrush TB = new SolidBrush(PushedTextColor))
                {
                    G.FillPath(BG, GP);
                    G.DrawPath(P, GP);
                    G.DrawString(Text, Font, TB, new Rectangle(0, 0, Width, Height), H.SetPosition());
                }

                break;
        }

        GP.Dispose();
    }

    #endregion

    #region  Events 

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        State = HelperMethods.MouseMode.Hovered;
        Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        State = HelperMethods.MouseMode.Pushed;
        Invalidate();
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        State = HelperMethods.MouseMode.Hovered;
        Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseEnter(e);
        State = HelperMethods.MouseMode.Normal;
        Invalidate();
    }

    #endregion

}

#endregion

#region  Alert 

[DefaultEvent("TextChanged")]
public class DelightAlert : Control
{

    #region  Declarations 

    private Color
        BaseColor,
        TitleColor,
        ContentColor,
        ImageAreaColor,
        ImageColor;
    private string _TitleText;
    private Style _AlertStyle;
    private int _RoundRadius;
    private static readonly HelperMethods H = new HelperMethods();

    #region  Images 

    private string Cross
    {
        get { return "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAA2ZpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYxIDY0LjE0MDk0OSwgMjAxMC8xMi8wNy0xMDo1NzowMSAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDoyMUJDRjc3RDYyMDRFMjExOEI4OEMwODMzOTc4RjA1OCIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDo1RTY2MjZCQjA3NjgxMUUyOTZGMUI2MjcyMzkwREZENiIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDo1RTY2MjZCQTA3NjgxMUUyOTZGMUI2MjcyMzkwREZENiIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ1M1LjEgV2luZG93cyI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkI4RTUzRUFDRTYwNkUyMTFBOTQ5RkEyRUM0RTE2OTk4IiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOjIxQkNGNzdENjIwNEUyMTE4Qjg4QzA4MzM5NzhGMDU4Ii8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+ec5RhAAAAedJREFUeNrs2ktqwzAQBuCoZNlVD2DoDQI9QFeFXrcn8Q0COUC32boWxNDGeo38zyP1CLQLw/wfiS0NCdM0Hfa8ng47Xw7gAA7gAA7gAA7gAA7gAA6wz3WsfSCE8FCBqLdb5DfgNO+BIVOs+8EqVtqEJr/nfQYjLHWv8/5E5FnlAwAsTU63jUK4r9uEIA1w3yQKIVe3iiAJkGtyK0KtbhFBCqDWZC9Ca90sggRAa5NUBGrdJAI3ALXJZV8qCL11VwicAL1N1hC21o0I7xIAr7cQExBha/i4x3m/SP0EkAjw8FIPQRQCPLzkaxCBAA8vfRDSQsiG1zgKSyMUw2tdhgYhhGp4LQAJhKbwmgCcCM3htQE4EEjhLQAgEcjhewA4psKx6WdDdURmgsizPeUWaeongA7fhaAFwBWejGBhKqyKYGUqrIZgaSqsgmBtKiyOYHEqnDvkXDgQrE6FUye8gQPB8lQ4dbyFI3ACjExnewTCaH0sXrvYbEGI38o3qWdAT6Ott7qe2n/CS70FKI1Sr7SU2qvwkueAlka77vONtZPhpU+CpUZ7w7fUzobXuAukGt0avlS7GN7CWBwVPlW7Gl57LP4FDv+79tgSvgcg1EL6HyX/+TqiRR9t+b/FHcABHMAB9rx+BBgAzGhrTT2ILiYAAAAASUVORK5CYII="; }
    }

    private string Alert
    {
        get { return "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAACXBIWXMAAAsTAAALEwEAmpwYAAAKT2lDQ1BQaG90b3Nob3AgSUNDIHByb2ZpbGUAAHjanVNnVFPpFj333vRCS4iAlEtvUhUIIFJCi4AUkSYqIQkQSoghodkVUcERRUUEG8igiAOOjoCMFVEsDIoK2AfkIaKOg6OIisr74Xuja9a89+bN/rXXPues852zzwfACAyWSDNRNYAMqUIeEeCDx8TG4eQuQIEKJHAAEAizZCFz/SMBAPh+PDwrIsAHvgABeNMLCADATZvAMByH/w/qQplcAYCEAcB0kThLCIAUAEB6jkKmAEBGAYCdmCZTAKAEAGDLY2LjAFAtAGAnf+bTAICd+Jl7AQBblCEVAaCRACATZYhEAGg7AKzPVopFAFgwABRmS8Q5ANgtADBJV2ZIALC3AMDOEAuyAAgMADBRiIUpAAR7AGDIIyN4AISZABRG8lc88SuuEOcqAAB4mbI8uSQ5RYFbCC1xB1dXLh4ozkkXKxQ2YQJhmkAuwnmZGTKBNA/g88wAAKCRFRHgg/P9eM4Ors7ONo62Dl8t6r8G/yJiYuP+5c+rcEAAAOF0ftH+LC+zGoA7BoBt/qIl7gRoXgugdfeLZrIPQLUAoOnaV/Nw+H48PEWhkLnZ2eXk5NhKxEJbYcpXff5nwl/AV/1s+X48/Pf14L7iJIEyXYFHBPjgwsz0TKUcz5IJhGLc5o9H/LcL//wd0yLESWK5WCoU41EScY5EmozzMqUiiUKSKcUl0v9k4t8s+wM+3zUAsGo+AXuRLahdYwP2SycQWHTA4vcAAPK7b8HUKAgDgGiD4c93/+8//UegJQCAZkmScQAAXkQkLlTKsz/HCAAARKCBKrBBG/TBGCzABhzBBdzBC/xgNoRCJMTCQhBCCmSAHHJgKayCQiiGzbAdKmAv1EAdNMBRaIaTcA4uwlW4Dj1wD/phCJ7BKLyBCQRByAgTYSHaiAFiilgjjggXmYX4IcFIBBKLJCDJiBRRIkuRNUgxUopUIFVIHfI9cgI5h1xGupE7yAAygvyGvEcxlIGyUT3UDLVDuag3GoRGogvQZHQxmo8WoJvQcrQaPYw2oefQq2gP2o8+Q8cwwOgYBzPEbDAuxsNCsTgsCZNjy7EirAyrxhqwVqwDu4n1Y8+xdwQSgUXACTYEd0IgYR5BSFhMWE7YSKggHCQ0EdoJNwkDhFHCJyKTqEu0JroR+cQYYjIxh1hILCPWEo8TLxB7iEPENyQSiUMyJ7mQAkmxpFTSEtJG0m5SI+ksqZs0SBojk8naZGuyBzmULCAryIXkneTD5DPkG+Qh8lsKnWJAcaT4U+IoUspqShnlEOU05QZlmDJBVaOaUt2ooVQRNY9aQq2htlKvUYeoEzR1mjnNgxZJS6WtopXTGmgXaPdpr+h0uhHdlR5Ol9BX0svpR+iX6AP0dwwNhhWDx4hnKBmbGAcYZxl3GK+YTKYZ04sZx1QwNzHrmOeZD5lvVVgqtip8FZHKCpVKlSaVGyovVKmqpqreqgtV81XLVI+pXlN9rkZVM1PjqQnUlqtVqp1Q61MbU2epO6iHqmeob1Q/pH5Z/YkGWcNMw09DpFGgsV/jvMYgC2MZs3gsIWsNq4Z1gTXEJrHN2Xx2KruY/R27iz2qqaE5QzNKM1ezUvOUZj8H45hx+Jx0TgnnKKeX836K3hTvKeIpG6Y0TLkxZVxrqpaXllirSKtRq0frvTau7aedpr1Fu1n7gQ5Bx0onXCdHZ4/OBZ3nU9lT3acKpxZNPTr1ri6qa6UbobtEd79up+6Ynr5egJ5Mb6feeb3n+hx9L/1U/W36p/VHDFgGswwkBtsMzhg8xTVxbzwdL8fb8VFDXcNAQ6VhlWGX4YSRudE8o9VGjUYPjGnGXOMk423GbcajJgYmISZLTepN7ppSTbmmKaY7TDtMx83MzaLN1pk1mz0x1zLnm+eb15vft2BaeFostqi2uGVJsuRaplnutrxuhVo5WaVYVVpds0atna0l1rutu6cRp7lOk06rntZnw7Dxtsm2qbcZsOXYBtuutm22fWFnYhdnt8Wuw+6TvZN9un2N/T0HDYfZDqsdWh1+c7RyFDpWOt6azpzuP33F9JbpL2dYzxDP2DPjthPLKcRpnVOb00dnF2e5c4PziIuJS4LLLpc+Lpsbxt3IveRKdPVxXeF60vWdm7Obwu2o26/uNu5p7ofcn8w0nymeWTNz0MPIQ+BR5dE/C5+VMGvfrH5PQ0+BZ7XnIy9jL5FXrdewt6V3qvdh7xc+9j5yn+M+4zw33jLeWV/MN8C3yLfLT8Nvnl+F30N/I/9k/3r/0QCngCUBZwOJgUGBWwL7+Hp8Ib+OPzrbZfay2e1BjKC5QRVBj4KtguXBrSFoyOyQrSH355jOkc5pDoVQfujW0Adh5mGLw34MJ4WHhVeGP45wiFga0TGXNXfR3ENz30T6RJZE3ptnMU85ry1KNSo+qi5qPNo3ujS6P8YuZlnM1VidWElsSxw5LiquNm5svt/87fOH4p3iC+N7F5gvyF1weaHOwvSFpxapLhIsOpZATIhOOJTwQRAqqBaMJfITdyWOCnnCHcJnIi/RNtGI2ENcKh5O8kgqTXqS7JG8NXkkxTOlLOW5hCepkLxMDUzdmzqeFpp2IG0yPTq9MYOSkZBxQqohTZO2Z+pn5mZ2y6xlhbL+xW6Lty8elQfJa7OQrAVZLQq2QqboVFoo1yoHsmdlV2a/zYnKOZarnivN7cyzytuQN5zvn//tEsIS4ZK2pYZLVy0dWOa9rGo5sjxxedsK4xUFK4ZWBqw8uIq2Km3VT6vtV5eufr0mek1rgV7ByoLBtQFr6wtVCuWFfevc1+1dT1gvWd+1YfqGnRs+FYmKrhTbF5cVf9go3HjlG4dvyr+Z3JS0qavEuWTPZtJm6ebeLZ5bDpaql+aXDm4N2dq0Dd9WtO319kXbL5fNKNu7g7ZDuaO/PLi8ZafJzs07P1SkVPRU+lQ27tLdtWHX+G7R7ht7vPY07NXbW7z3/T7JvttVAVVN1WbVZftJ+7P3P66Jqun4lvttXa1ObXHtxwPSA/0HIw6217nU1R3SPVRSj9Yr60cOxx++/p3vdy0NNg1VjZzG4iNwRHnk6fcJ3/ceDTradox7rOEH0x92HWcdL2pCmvKaRptTmvtbYlu6T8w+0dbq3nr8R9sfD5w0PFl5SvNUyWna6YLTk2fyz4ydlZ19fi753GDborZ752PO32oPb++6EHTh0kX/i+c7vDvOXPK4dPKy2+UTV7hXmq86X23qdOo8/pPTT8e7nLuarrlca7nuer21e2b36RueN87d9L158Rb/1tWeOT3dvfN6b/fF9/XfFt1+cif9zsu72Xcn7q28T7xf9EDtQdlD3YfVP1v+3Njv3H9qwHeg89HcR/cGhYPP/pH1jw9DBY+Zj8uGDYbrnjg+OTniP3L96fynQ89kzyaeF/6i/suuFxYvfvjV69fO0ZjRoZfyl5O/bXyl/erA6xmv28bCxh6+yXgzMV70VvvtwXfcdx3vo98PT+R8IH8o/2j5sfVT0Kf7kxmTk/8EA5jz/GMzLdsAAAAgY0hSTQAAeiUAAICDAAD5/wAAgOkAAHUwAADqYAAAOpgAABdvkl/FRgAAAx5JREFUeNrsW8+HW1EU/iZDCKE8hhCyyqqUrGYVyhCGtyqhhNmW0NWsQuiqzHYYsi4VSrehf0DIagjDVJguQymzmhFCSBdzXptO8+4998e7ee++HA5j5r5zzvfdN/fce859B+v1GnmWAnIuewL2BOwJ2BOwF8dSAvAewBjAinRMvyv5Dr4G4BrAOkavaYyXUgRwKwAf6YzGeic9BvhIe76BrwJ4UCDggZ7xRoYK4CMd+gK+qQE+0mbWwR8CmBoQMCUbmZWOAfhIz7IKvgxgboGAOdnKnFxYAB/pRdbA1wEsBYC+AWgBqJC2AIwE45dkMzMSB2YBIBQ8F9KYbc+OsgL+teGCdiZ4/iTLae+BmdIOBbvG1KfFrmD2finY+Smw000r+ADAvSDwlYIt0QJ6T75SJwNGOguYRMrsDNIG/iXNsCxwTiqrM+ysyGfq095zPWbYOmbaSk1aDBV2dKcMe6cK9kLT4E2rwkUAlwrjjyyNieTStHxmSsC54hY1sDRmc7043xUBFQB9jVRpkwBQDNVdEPBB45h6lAABZY2JMJYGM+09168M21807K4oJmdvwJXmnjyw9JZsOz9cZaHMNWXYN6khdtJe5pozfJjaT7R81oNZaWvB8LEw9JHYgqja3YlTUQe4ZMF+Yl2lIewUOKsSkm34sN5VasJehbchSa+2/LC6SoUdpJjA4ibIOFVzCOjobjI08rxNAhpgFGFlBJRhvylRkawBNuWjLC3KCOgnEFRL8DfbZe+qSVqUdXdMtL3FXzshX9pdpVFCAUVB9fF0IapGG6xlgv5GOq/p2jNtqZysph4SwO4qdT0Ez+4qybo7WVdpV2ngKJA5gLeUpqr089yR79iu0ivNMpeqzmJmIQDvJqmpxpbPRo5moC14A984iuG/tBg6/D98Idl6u4oj3Ozu3OWQgDsAxQKAd3B7AelE85xgW+qEHRPHqeh2x4vgpk6Q8B5clAbb+HtNru0wDf5zJjkgArz8QIEhjwUA35Ff+VEA8CnHBHyO0uDM4/0/69ukGoCbHIG/wZav00p4um0x9hj4hDD+6U79HgCQj4+UlrnQ0gAAAABJRU5ErkJggg=="; }
    }

    private string Info
    {
        get { return "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAA2ZpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDpGNzdGMTE3NDA3MjA2ODExOEMxNDkyODc0N0NBMUEwNCIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDoyRDZBQTcyMjNERkQxMUUyQUZGMEFDRjY0RjNFODlDOCIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDoyRDZBQTcyMTNERkQxMUUyQUZGMEFDRjY0RjNFODlDOCIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ1M1IE1hY2ludG9zaCI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkZBN0YxMTc0MDcyMDY4MTE4MDgzRkQyMTE2MTM0QUNBIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkY3N0YxMTc0MDcyMDY4MTE4QzE0OTI4NzQ3Q0ExQTA0Ii8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+nsz5iAAAAdpJREFUeNpi/P//PwM+EBkVw0AE4ATiCiAOBGJ1ID4LxE1AvGP5siV4NbIwUA74gfgAEBsgiVkC8XYgDgXiNfg0M1HBAXVolqPLMdDaAVF45NTp4QAhPHLv6OGAm3jkVtLDAfU4xC8DcSU9HLAeiIOA+BSUfw2IO4DYHIi/E9JMjWwIc8R6cjQyMQwwINcBzEDsDcSLgPgMEP8B4ldAPBOIeUgxiJwoUATiFUBshiYuCsRp0GI5jlYhALLgEhbLkYEfLUIAFOTzgTiWCLW/aJEG5kAtfwoNXhkg/oJD7TVqh0ApECcA8UEgjgDiF0AsB41rbGArtUMgH4gXA7Eb1HKYo5ixqP0LxMuoHQKqaCWaEDREsIEd0GiiagigF6eZePL6XFoXRKB4z8EhB4qeTbR2ACgnSOCQWwxNAzRzACjRFeGRJzn4SXWAN54m1hECDROqOCAZj9wyBjIBsQ4QhYYAvvYADLhCa0mqOiAER8HDAM33sAKqCNofEKJ2ZeRNIHEmQHOIE9QxWdR0ACfUYFxAAlpTwkLDGYgfUTMKtPBUPOhR4UxqbiDGAfxEqAHlAkNysiIxDriJp3l9H4g9gTgaiF/TKhs+hfZyL0P5j6C1HshSPSibbAAQYADY7VOTcX9dkwAAAABJRU5ErkJggg=="; }
    }

    private string Tick
    {
        get { return "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAA2ZpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYxIDY0LjE0MDk0OSwgMjAxMC8xMi8wNy0xMDo1NzowMSAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDoyMUJDRjc3RDYyMDRFMjExOEI4OEMwODMzOTc4RjA1OCIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDoyMjZBMEM2MTA3NjgxMUUyODE1RDg4RUZCRjZENTY0MCIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDoyMjZBMEM2MDA3NjgxMUUyODE1RDg4RUZCRjZENTY0MCIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ1M1LjEgV2luZG93cyI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkI4RTUzRUFDRTYwNkUyMTFBOTQ5RkEyRUM0RTE2OTk4IiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOjIxQkNGNzdENjIwNEUyMTE4Qjg4QzA4MzM5NzhGMDU4Ii8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+BfpfogAAAbpJREFUeNrs2tFNwzAQxvGadAUknpDYgBEYhUm8SudhCRC8wBBIwUaNhEIcO/Z9d7n4LN0TQvj/a4DGqRvH8dTzujl1vgzAAAzAAAzAAAzAAAzAADpd59QXnHOHiVy74dN8Bdz2/CtwH+YlzIXk8liance/xq1f51Lb+NupDGAeX4RwFIBUfBbhCAC5+FUE7QCl8UkEzQBb4xcRtALUxv9D0AjQGh/nPcydRgCq+AeNVwB5vCYASLwWAFi8BgBoPAfA457j0QA+zHeY573GIwH8n41sRWCLRwH4hQ2VIrDGIwD8ysZyCOzx1AC+YIMpBJF4SgC/YaNzBLF4KgBfseEJQTSeAsA3bDwifErG5wByx+JD4w8fplvSyvUR5inMG/SpSeYKGK6HCyPzNL/ylH8DuBHI4in/C3AhkMZTvw9AI5DHI94JohAg8ah7AWoEWDzybpAKARqPPg9oRYDHc5wI1SKwxHOdCW5FYIvnPBQtRWCN5z4VziGwx0sci6cQROKlngvMEcTiJR+MTAii8TkAl4ol+qBkRIif5/uSBkgtNMAu1hrAueabjrTs0+IGYAAGYAAG0PH6EWAASdEo9qjTEtIAAAAASUVORK5CYII="; }
    }

    private string Bubble
    {
        get { return "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAA2ZpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDpGNzdGMTE3NDA3MjA2ODExOEMxNDkyODc0N0NBMUEwNCIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDoyRDI3NDg3RjNERkQxMUUyQUZGMEFDRjY0RjNFODlDOCIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDoyRDI3NDg3RTNERkQxMUUyQUZGMEFDRjY0RjNFODlDOCIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ1M1IE1hY2ludG9zaCI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkZBN0YxMTc0MDcyMDY4MTE4MDgzRkQyMTE2MTM0QUNBIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkY3N0YxMTc0MDcyMDY4MTE4QzE0OTI4NzQ3Q0ExQTA0Ii8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+5LTneAAAAbpJREFUeNpi/P//P8NAApbIqBhy9bIBsS6UfRmIf5HlACLVyQGxHxDbALEllI8NPALi40B8BIg3QfkUOSAQiNOA2IMEh4JwOBBPBuLdQDwdiNfj0sCEQxwUtMeAeB0JlmMDrlAzTgKxAbEOqAPi89CgphYwA+IzULNxOoAZiKcBcSOUTW3ADDV7JrL5yA5oB+JMOuS8NKhdKA4AxXMpHbN/KSxtMUGDY+YAlEHgqAA5wBtPvqYlANnpDXJAwgCWxAlM0CwyUMAM5ADRAXSAKBO5lQiVwC+QA+4MoAPuM0FrroEC+0AOmDuADpgOcsAFfNUlDQGovXATVhRnAfE7Olr+GojTkeuCF0DsA8Tf6ZHygTgaaidKbQhqSnkC8UcaWv4FiN2gLSWsDZKDQGwCihsaWA5Ka+ZQO/C2iO7AgodKAJS2CqAeu0ZMoxRUPRtTwWKQT2cB8Rp8pS0LjgYpDxZxUALtg6ZgLSBWAWJ+pET8FCp3AWr5a3L7Bdh8vxWIc0FFJ9V7RljELNE6GrnQQoMmgAlHE/ovEHcDsTYtLccWAjzQVGsI7e/RvnOKpZRyoGeFwDjQ3XOAAAMAtPdUOngwWngAAAAASUVORK5CYII="; }
    }

    private string _img = null;

    #endregion

    #endregion

    #region  Constructors 

    public DelightAlert()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        Cursor = Cursors.Arrow;
        BackColor = Color.Transparent;
        Font = new Font("Segoe UI", 9, FontStyle.Regular);
        UpdateStyles();
        _AlertStyle = Style.Success;
        BaseColor = Colors.AlertSuccessBase;
        TitleColor = Colors.AlertSuccessTitle;
        ContentColor = Colors.AlertSuccessContent;
        ImageAreaColor = Colors.AlertSuccessImageArea;
        ImageColor = Colors.AlertSuccessImage;
        _img = Tick;
        TitleText = "Success Message Title";
        _RoundRadius = 2;
    }

    #endregion

    #region  Properties 

    [Category("Custom Properties"), Description("Gets or sets the control title text.")]
    public string TitleText
    {
        get { return _TitleText; }
        set
        {
            _TitleText = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the control alert style.")]
    public Style AlertStyle
    {
        get { return _AlertStyle; }
        set
        {
            _AlertStyle = value;
            switch (value)
            {
                case Style.Success:
                    BaseColor = Colors.AlertSuccessBase;
                    TitleColor = Colors.AlertSuccessTitle;
                    ContentColor = Colors.AlertSuccessContent;
                    ImageAreaColor = Colors.AlertSuccessImageArea;
                    ImageColor = Colors.AlertSuccessImage;
                    _img = Tick;
                    TitleText = "Success Message Title";
                    break;
                case Style.Notice:
                    BaseColor = Colors.AlertNoticeBase;
                    TitleColor = Colors.AlertNoticeTitle;
                    ContentColor = Colors.AlertNoticeContent;
                    ImageAreaColor = Colors.AlertNoticeImageArea;
                    ImageColor = Colors.AlertNoticeImage;
                    TitleText = "Notice Message Title";
                    _img = Info;
                    break;
                case Style.Info:
                    BaseColor = Colors.AlertInfoBase;
                    TitleColor = Colors.AlertInfoTitle;
                    ContentColor = Colors.AlertInfoContent;
                    ImageAreaColor = Colors.AlertInfoImageArea;
                    ImageColor = Colors.AlertInfoImage;
                    TitleText = "Info Message Title";
                    _img = Bubble;
                    break;
                case Style.Warning:
                    BaseColor = Colors.AlertWarningBase;
                    TitleColor = Colors.AlertWarningTitle;
                    ContentColor = Colors.AlertWarningContent;
                    ImageAreaColor = Colors.AlertWarningImageArea;
                    ImageColor = Colors.AlertWarningImage;
                    TitleText = "Warning Message Title";
                    _img = Alert;
                    break;
                case Style.Danger:
                    BaseColor = Colors.AlertDangerBase;
                    TitleColor = Colors.AlertDangerTitle;
                    ContentColor = Colors.AlertDangerContent;
                    ImageAreaColor = Colors.AlertDangerImageArea;
                    ImageColor = Colors.AlertDangerImage;
                    TitleText = "Danger Message Title";
                    _img = Cross;
                    break;
            }
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether the control can Rounded in corners.")]
    public int RoundRadius
    {
        get { return _RoundRadius; }
        set
        {
            _RoundRadius = value;
            Invalidate();
        }
    }

    #endregion

    #region  Ennumerators 

    public enum Style
    {
        Success,
        Info,
        Notice,
        Warning,
        Danger
    }

    #endregion

    #region  Draw Control 

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;
        GraphicsPath GP = new GraphicsPath();
        GraphicsPath GPSmall = new GraphicsPath();
        Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);

        if (RoundRadius > 0)
        {
            G.SmoothingMode = SmoothingMode.AntiAlias;
            GP = H.RoundRec(Rect, RoundRadius);
            GPSmall = H.RoundRec(new Rectangle(1, 1, 4, Height - 2), RoundRadius, true, false, true, false);
        }
        else
        {
            GP.AddRectangle(Rect);
            GPSmall.AddRectangle(new Rectangle(1, 1, 4, Height - 2));
        }

        GP.CloseFigure();

        G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;


        using (SolidBrush BG = new SolidBrush(BaseColor))
        using (SolidBrush TB = new SolidBrush(TitleColor))
        using (SolidBrush CC = new SolidBrush(ContentColor))
        using (Font TTF = new Font("Segoe UI", 9, FontStyle.Bold))
        {
            G.FillPath(BG, GP);
            H.FillRoundedPath(G, ImageAreaColor, new Rectangle(4, 7, 32, 32), 4);
            G.DrawString(TitleText, TTF, TB, new Rectangle(45, 5, Width - 10, 20));
            G.DrawString(Text, Font, CC, 45, G.MeasureString(TitleText, TTF).Height + 8);
            G.InterpolationMode = InterpolationMode.HighQualityBicubic;

            H.DrawImageWithColor(G, new Rectangle(8, 10, 24, 24), _img, ImageColor);

        }

        GP.Dispose();
        GPSmall.Dispose();
    }

    #endregion

    #region  Events 

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Height = 50;
    }

    #endregion

}

#endregion

#region  CheckBox 

[DefaultEvent("CheckedChanged"), DefaultProperty("Checked")]
public class DelightCheckBox : Control
{

    #region  Declarations 

    private bool _Checked;
    protected HelperMethods.MouseMode State = HelperMethods.MouseMode.Normal;
    private Color _UnCheckBorderColor = Colors.UncheckedBorder;
    private Color _CheckedBorderColor = Colors.CheckedBorder;
    private Color _CheckSymbolColor = Color.White;
    private Color _UnCheckedBaseColor = Colors.UncheckedBase;
    private Color _CheckedBaseColor = Colors.CheckedBase;
    private static readonly HelperMethods H = new HelperMethods();

    #endregion

    #region  Properties 

    [Category("Custom Properties"), Description("Gets or set a value indicating whether the control is in the checked state.")]
    public bool Checked
    {
        get { return _Checked; }
        set
        {
            _Checked = value;
            CheckedChanged?.Invoke(this);
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the control border color while unchecked.")]
    public Color UnCheckBorderColor
    {
        get { return _UnCheckBorderColor; }
        set
        {
            _UnCheckBorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the control border color while Checked.")]
    public Color CheckBorderColor
    {
        get { return _CheckedBorderColor; }
        set
        {
            _CheckedBorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the control check symbol color while checked.")]
    public Color CheckSymbolColor
    {
        get { return _CheckSymbolColor; }
        set
        {
            _CheckSymbolColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the background color of control while un-checked.")]
    public Color UnCheckedBaseColor
    {
        get { return _UnCheckedBaseColor; }
        set
        {
            _UnCheckedBaseColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the background color of control while checked.")]
    public Color CheckedBaseColor
    {
        get { return _CheckedBaseColor; }
        set
        {
            _CheckedBaseColor = value;
            Invalidate();
        }
    }

    #endregion

    #region  Constructors 

    public DelightCheckBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
        DoubleBuffered = true;
        Cursor = Cursors.Hand;
        BackColor = Color.Transparent;
        ForeColor = Colors.Un_CheckedText;
        Font = new Font("Segoe UI", 9);
        UpdateStyles();
    }

    #endregion

    #region  Draw Control 

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        Rectangle rect = new Rectangle(0, 0, 18, 17);
        using (SolidBrush BackBrush = new SolidBrush(Checked ? CheckedBaseColor : UnCheckedBaseColor))
        using (Pen CheckMarkPen = new Pen(CheckSymbolColor, 2))
        using (SolidBrush TC = new SolidBrush(ForeColor))
        using (Pen BorderPen = new Pen(Checked ? CheckBorderColor : UnCheckBorderColor))
        {
            G.FillRectangle(BackBrush, rect);
            if (Checked)
                G.DrawLines(CheckMarkPen, new Point[] {
                                    new Point(4, 8),
                                    new Point(7, 11),
                                    new Point(14, 4)
                                });
            G.DrawRectangle(BorderPen, new Rectangle(0, 0, 17, 16));
            G.DrawString(Text, Font, TC, new Rectangle(18, 2, Width, Height - 4), H.SetPosition(StringAlignment.Near));
        }
      

    }

    #endregion

    #region  Events 

    public event CheckedChangedEventHandler CheckedChanged;
    public delegate void CheckedChangedEventHandler(object sender);

    protected override void OnClick(EventArgs e)
    {
        _Checked = !Checked;
        CheckedChanged?.Invoke(this);
        base.OnClick(e);
        Invalidate();
    }

    protected override void OnTextChanged(System.EventArgs e)
    {
        Invalidate();
        base.OnTextChanged(e);
    }

    protected override void OnResize(System.EventArgs e)
    {
        base.OnResize(e);
        Height = 17;
        Invalidate();
    }

    protected override void OnMouseHover(EventArgs e)
    {
        base.OnMouseHover(e);
        State = HelperMethods.MouseMode.Hovered;
        Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        State = HelperMethods.MouseMode.Normal;
        Invalidate();
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
    }

    #endregion

}

#endregion

#region  RadioButton 

[DefaultEvent("CheckedChanged"), DefaultProperty("Checked")]
public class DelightRadioButton : Control
{

    #region  Declarations 

    private bool _Checked;
    protected int _Group = 1;
    protected HelperMethods.MouseMode State = HelperMethods.MouseMode.Normal;
    private Color _UnCheckBorderColor = Colors.UncheckedBorder;
    private Color _CheckSymbolColor = Colors.White;
    private Color _UnCheckedBaseColor = Colors.UncheckedBase;
    private Color _CheckedBaseColor = Colors.CheckedBase;
    private Color _CheckedBorderColor = Colors.CheckedBorder;
    private static readonly HelperMethods H = new HelperMethods();

    #endregion

    #region  Properties 

    [Category("Custom Properties"), Description("Gets or set a value indicating whether the control is in the checked state.")]
    public bool Checked
    {
        get { return _Checked; }
        set
        {
            _Checked = value;
            CheckedChanged?.Invoke(this);
            Invalidate();
        }
    }

    [Category("Custom Properties")]
    public int Group
    {
        get { return _Group; }
        set
        {
            _Group = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the control border color while unchecked.")]
    public Color UnCheckBorderColor
    {
        get { return _UnCheckBorderColor; }
        set
        {
            _UnCheckBorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the control border color while Checked.")]
    public Color CheckBorderColor
    {
        get { return _CheckedBorderColor; }
        set
        {
            _CheckedBorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the control check symbol color while checked.")]
    public Color CheckSymbolColor
    {
        get { return _CheckSymbolColor; }
        set
        {
            _CheckSymbolColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the background color of control while un-checked.")]
    public Color UnCheckedBaseColor
    {
        get { return _UnCheckedBaseColor; }
        set
        {
            _UnCheckedBaseColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the background color of control while checked.")]
    public Color CheckedBaseColor
    {
        get { return _CheckedBaseColor; }
        set
        {
            _CheckedBaseColor = value;
            Invalidate();
        }
    }

    #endregion

    #region  Constructors 

    public DelightRadioButton()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
        DoubleBuffered = true;
        Cursor = Cursors.Hand;
        BackColor = Color.Transparent;
        ForeColor = Colors.Un_CheckedText;
        Font = new Font("Segoe UI", 9, FontStyle.Regular);
        UpdateStyles();
    }

    #endregion

    #region  Draw Control 

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;
        G.SmoothingMode = SmoothingMode.AntiAlias;
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        using (SolidBrush BackBrush = new SolidBrush(Checked ? CheckedBaseColor : UnCheckedBaseColor))
        using (SolidBrush CheckMarkBrush = new SolidBrush(CheckSymbolColor))
        using (SolidBrush FC = new SolidBrush(ForeColor))
        using (Pen BorderPen = new Pen(Checked ? CheckBorderColor : UnCheckBorderColor))
        {
            G.FillEllipse(BackBrush, new Rectangle(0, 0, 21, 21));
            if (Checked)
            {
                G.FillEllipse(CheckMarkBrush, new Rectangle(5, 5, 10, 10));
            }
            G.DrawString(Text, Font, FC, new Rectangle(21, 1, Width, Height - 2), H.SetPosition(StringAlignment.Near));
            G.DrawEllipse(BorderPen, new Rectangle(0, 0, 20, 20));
        }
       

    }

    #endregion

    #region  Events 

    public event CheckedChangedEventHandler CheckedChanged;
    public delegate void CheckedChangedEventHandler(object sender);

    private void UpdateState()
    {
        if (!IsHandleCreated || !Checked)
            return;
        foreach (Control C in Parent.Controls)
        {
            if (!object.ReferenceEquals(C, this) && C is DelightRadioButton && ((DelightRadioButton)C).Group == _Group)
            {
                ((DelightRadioButton)C).Checked = false;
            }
        }
        CheckedChanged?.Invoke(this);
    }

    protected override void OnClick(EventArgs e)
    {
        _Checked = !Checked;
        UpdateState();
        base.OnClick(e);
        Invalidate();
    }

    protected override void OnCreateControl()
    {
        UpdateState();
        base.OnCreateControl();
    }

    protected override void OnTextChanged(System.EventArgs e)
    {
        Invalidate();
        base.OnTextChanged(e);
    }

    protected override void OnResize(System.EventArgs e)
    {
        base.OnResize(e);
        Height = 22;
        Invalidate();
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
    }

    #endregion

}

#endregion

#region  Seperator 

public class DelightSeperator : Control
{

    #region  Variables 

    private Style _SepStyle = Style.Horizental;

    private Color _SeperatorColor = Colors.Seperator;
    #endregion

    #region  Enumerators 

    public enum Style
    {
        Horizental,
        Vertiacal
    }

    #endregion

    #region  Properties 

    [Category("Custom Properties"), Description("Gets or sets the style for the control.")]
    public Style SeperatorStyle
    {
        get { return _SepStyle; }
        set
        {
            _SepStyle = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the color for the control.")]
    public Color SeperatorColor
    {
        get { return _SeperatorColor; }
        set
        {
            _SeperatorColor = value;
            Invalidate();
        }
    }

    #endregion

    #region  Constructors 

    public DelightSeperator()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
        DoubleBuffered = true;
        UpdateStyles();
        BackColor = Color.Transparent;
    }

    #endregion

    #region  Draw Control 

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;
        using (Pen P = new Pen(SeperatorColor))
        {
            switch (SeperatorStyle)
            {
                case Style.Horizental:
                    G.DrawLine(P, 0, 1, Width, 1);
                    break;
                case Style.Vertiacal:
                    G.DrawLine(P, 1, 0, 1, Height);
                    break;
            }
        }

    }

    #endregion

    #region  Events 

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        if (SeperatorStyle == Style.Horizental)
        {
            Height = 4;
        }
        else
        {
            Width = 4;
        }
    }

    #endregion

}

#endregion

#region  Label 

[DefaultEvent("TextChanged")]
public class DelightLabel : Control
{

    #region  Draw Cotnrol 

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        using (SolidBrush TB = new SolidBrush(ForeColor))
        {
            e.Graphics.DrawString(Text, Font, TB, ClientRectangle);
        }
    }

    #endregion

    #region  Constructors 

    public DelightLabel()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        DoubleBuffered = true;
        UpdateStyles();
        BackColor = Color.Transparent;
        ForeColor = Colors.Label;
        Font = new Font("Segoe UI", 10);
    }

    #endregion

    #region  Events 

    public new event TextChangedEventHandler TextChanged;
    public delegate void TextChangedEventHandler(object sender);

    protected override void OnResize(EventArgs e)
    {
        Height = Font.Height;
    }

    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
        TextChanged?.Invoke(this);
        Invalidate();
    }

    #endregion

}

#endregion

#region  Link Label 
class DelightLinkLabel : LinkLabel
{

    #region  Constructors 

    public DelightLinkLabel()
    {
        Font = new Font("Segoe UI", 9);
        BackColor = Color.Transparent;
        LinkColor = Colors.LinkNormal;
        ActiveLinkColor = Colors.LinkActive;
        VisitedLinkColor = Colors.LinkVisited;
        LinkBehavior = LinkBehavior.HoverUnderline;
    }

    #endregion

}

#endregion

#region Progress

[DefaultEvent("ValueChanged"), DefaultProperty("Value")]
public class DelightProgressBar : Control
{

    #region Declarations

    private static readonly HelperMethods H = new HelperMethods();
    private int _Maximum = 100;
    private int _Value = 0;
    private Color _ProgressColor = Colors.Progress;
    private Color _BaseColor = Colors.ProgressBase;
    private Color _BorderColor = Colors.ProgressBorder;
    private int CurrentValue;
    private int _RoundRadius = 0;

    #endregion

    #region Constructors

    public DelightProgressBar()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        CurrentValue = Convert.ToInt32(Value / Maximum * Width);
        UpdateStyles();

    }

    #endregion

    #region Properties

    [Category("Custom Properties"), Description("Gets or sets the current position of the progressbar.")]
    public int Value
    {
        get
        {
            if (_Value < 0)
            {
                return 0;
            }
            else
            {
                return _Value;
            }
        }
        set
        {
            if (value > Maximum)
            {
                value = Maximum;
            }
            _Value = value;
            RenewCurrentValue();
            Invalidate();
            ValueChanged?.Invoke(this);
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the maximum value of the progressbar.")]
    public int Maximum
    {
        get { return _Maximum; }
        set
        {
            if (value < _Value)
            {
                _Value = Value;
            }
            _Maximum = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the basecolor of the control.")]
    public Color BaseColor
    {
        get { return _BaseColor; }
        set
        {
            _BaseColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the border color of the control.")]
    public Color BorderColor
    {
        get { return _BorderColor; }
        set
        {
            _BorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the progress color of the control.")]
    public Color ProgressColor
    {
        get { return _ProgressColor; }
        set
        {
            _ProgressColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether the control can Rounded in corners.")]
    public int RoundRadius
    {
        get { return _RoundRadius; }
        set
        {
            _RoundRadius = value;
            Invalidate();
        }
    }

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;
        GraphicsPath GP = new GraphicsPath();

        CurrentValue = (int)Math.Round(Value / (double)Maximum * Width);
        Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);

        if (RoundRadius > 0)
        {
            G.SmoothingMode = SmoothingMode.AntiAlias;
            GP = H.RoundRec(Rect, RoundRadius);
        }
        else
        {
            GP.AddRectangle(Rect);
        }

        GP.CloseFigure();

        using (SolidBrush BG = new SolidBrush(BaseColor))
        {
            G.FillPath(BG, GP);
        }

        if (CurrentValue != 0)
        {
            using (SolidBrush PS = new SolidBrush(ProgressColor))
            {
                GraphicsPath GP2 = new GraphicsPath();
                if (RoundRadius > 0)
                {
                    G.SmoothingMode = SmoothingMode.AntiAlias;
                    GP2 = H.RoundRec(new Rectangle(Rect.X, Rect.Y, CurrentValue - 1, Rect.Height), RoundRadius);
                }
                else
                {
                    GP2.AddRectangle(new Rectangle(Rect.X, Rect.Y, CurrentValue - 1, Rect.Height));
                }
                GP2.CloseFigure();
                G.FillPath(PS, GP2);
                GP2.Dispose();
            }

        }

        using (Pen P = new Pen(BorderColor))
        {
            G.DrawPath(P, GP);
        }

        GP.Dispose();
    }

    #endregion

    #region Events

    public event ValueChangedEventHandler ValueChanged;
    public delegate void ValueChangedEventHandler(object sender);
    public void RenewCurrentValue()
    {
        CurrentValue = (int)Math.Round(Value / (double)Maximum * Width);
    }

    #endregion

}

[DefaultEvent("ValueChanged"), DefaultProperty("Value")]
public class DelightVerticalProgressBar : Control
{

    #region Declarations

    private static readonly HelperMethods H = new HelperMethods();
    private int _Maximum = 100;
    private int _Value = 0;
    private Color _ProgressColor = Colors.Progress;
    private Color _BaseColor = Colors.ProgressBase;
    private Color _BorderColor = Colors.ProgressBorder;
    private int CurrentValue;
    private int _RoundRadius = 0;

    #endregion

    #region Constructors


    public DelightVerticalProgressBar()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        RenewCurrentValue();
        UpdateStyles();

    }

    #endregion

    #region Properties

    [Category("Custom Properties"), Description("Gets or sets the current position of the progressbar.")]
    public int Value
    {
        get
        {
            if (_Value < 0)
            {
                return 0;
            }
            else
            {
                return _Value;
            }
        }
        set
        {
            if (value > Maximum)
            {
                value = Maximum;
            }
            _Value = value;
            RenewCurrentValue();
            Invalidate();
            ValueChanged?.Invoke(this);
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the maximum value of the progressbar.")]
    public int Maximum
    {
        get { return _Maximum; }
        set
        {
            if (value < _Value)
            {
                _Value = Value;
            }
            _Maximum = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the basecolor of the control.")]
    public Color BaseColor
    {
        get { return _BaseColor; }
        set
        {
            _BaseColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the border color of the control.")]
    public Color BorderColor
    {
        get { return _BorderColor; }
        set
        {
            _BorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the progress color of the control.")]
    public Color ProgressColor
    {
        get { return _ProgressColor; }
        set
        {
            _ProgressColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether the control can Rounded in corners.")]
    public int RoundRadius
    {
        get { return _RoundRadius; }
        set
        {
            _RoundRadius = value;
            Invalidate();
        }
    }

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;
        GraphicsPath GP = new GraphicsPath();
        CurrentValue = Convert.ToInt32((((double)Value) / ((double)Maximum)) * Height);
        Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);

        if (RoundRadius > 0)
        {
            G.SmoothingMode = SmoothingMode.AntiAlias;
            GP = H.RoundRec(Rect, RoundRadius);
        }
        else
        {
            GP.AddRectangle(Rect);
        }

        GP.CloseFigure();

        using (SolidBrush BG = new SolidBrush(BaseColor))
        {
            G.FillPath(BG, GP);
        }

        if (CurrentValue != 0)
        {
            GraphicsPath GP2 = new GraphicsPath();
            if (RoundRadius > 0)
            {
                G.SmoothingMode = SmoothingMode.AntiAlias;
                GP2 = H.RoundRec(new Rectangle(0, (Height - CurrentValue) - 1, Width, CurrentValue), RoundRadius);
            }
            else
            {
                GP2.AddRectangle(new Rectangle(0, Height - CurrentValue, Width, CurrentValue));
            }
            GP2.CloseFigure();
            using (SolidBrush PS = new SolidBrush(ProgressColor))
            {
                G.FillPath(PS, GP2);
            }
            GP2.Dispose();
        }
        using (Pen P = new Pen(BorderColor))
        {
            G.DrawPath(P, GP);
        }
        GP.Dispose();
    }

    #endregion

    #region Events

    public event ValueChangedEventHandler ValueChanged;
    public delegate void ValueChangedEventHandler(object sender);
    public void RenewCurrentValue()
    {

        CurrentValue = Convert.ToInt32((((double)Value) / ((double)Maximum)) * Height);
    }

    #endregion

}

#region CircleProgressbar
[DefaultEvent("ValueChanged"), DefaultProperty("Value")]
public class DelightCircleProgressBar : Control
{

    #region Declarations

    private static readonly HelperMethods H = new HelperMethods();
    private Color _ProgressColor = Colors.Progress;
    private Color _BaseColor = Colors.ProgressBorder;
    private Color _ProgressTextColor = Colors.Progress;
    private int _Maximum = 100;
    private int _Value = 0;
    private int _Thickness = 12;
    private bool _ShowProgressValue = true;
    private bool _FillInside = false;
    private Color _InsideColor = Colors.ProgressBase;
    private Font _Font = new Font("Segoe UI", 12);
    public event ValueChangedEventHandler ValueChanged;
    public delegate void ValueChangedEventHandler(object sender);
    private LineCap _EndStyle = LineCap.Custom;
    private LineCap _StartStyle = LineCap.Custom;
    private bool _ShowBase = true;

    #endregion

    #region Constructors

    public DelightCircleProgressBar()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        UpdateStyles();
        BackColor = Color.Transparent;
        Size = new Size(95, 95);
    }

    #endregion

    #region Properties

    [Category("Custom Properties"), Description("Gets or sets the current position of the CircleProgressBar.")]
    public int Value
    {
        get
        {
            switch (_Value)
            {
                case 0:
                    return 0;
                default:
                    return _Value;
            }
        }
        set
        {
            if (value > Maximum)
            {
                _Value = Maximum;
            }
            _Value = value;
            ValueChanged?.Invoke(this);
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets the Maximum value of the CircleProgressBar.")]
    public int Maximum
    {
        get { return _Maximum; }
        set
        {
            if (value < _Value)
            {
                _Value = Value;
            }
            _Maximum = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the progress color of the CircleProgressBar.")]
    public Color ProgressColor
    {
        get { return _ProgressColor; }
        set
        {
            _ProgressColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the progress text color of the CircleProgressBar.")]
    public Color ProgressTextColor
    {
        get { return _ProgressTextColor; }
        set
        {
            _ProgressTextColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the thickness of the CircleProgressBar.")]
    public virtual int Thickness
    {
        get { return _Thickness; }
        set
        {
            _Thickness = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the basecolor of the CircleProgressBar.")]
    public Color BaseColor
    {
        get { return _BaseColor; }
        set
        {
            _BaseColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets whether the inside of progress be shown or not.")]
    public bool FillInside
    {
        get { return _FillInside; }
        set
        {
            _FillInside = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets whether the base of progress be shown or not.")]
    public bool ShowBase
    {
        get { return _ShowBase; }
        set
        {
            _ShowBase = value;
            Invalidate();
        }
    }
    [Category("Custom Properties"), Description("Gets or sets whether the progress be shown as text or not.")]
    public bool ShowProgressValue
    {
        get { return _ShowProgressValue; }
        set
        {
            _ShowProgressValue = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the inside color of the CircleProgressBar.")]
    public Color InsideColor
    {
        get { return _InsideColor; }
        set
        {
            _InsideColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the font style applied to the calendar.")]
    public override Font Font
    {
        get { return _Font; }
        set
        {
            _Font = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the end shape of progress.")]
    public LineCap EndStyle
    {
        get { return _EndStyle; }
        set
        {
            _EndStyle = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the start shape of progress.")]
    public LineCap StartStyle
    {
        get { return _StartStyle; }
        set
        {
            _StartStyle = value;
            Invalidate();
        }
    }

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        G.SmoothingMode = SmoothingMode.HighQuality;
        Rectangle Rect = new Rectangle(5, 5, Width - 10, Height - 10);
        Rectangle PGA = new Rectangle(Convert.ToInt32(Thickness / 2) + 10, Convert.ToInt32(Thickness / 2) + 10, Width - Thickness - 19, Height - Thickness - 19);
        int CurrentValue = (int)Math.Round((double)(3.6 * Value));
        using (Pen BG = new Pen(BaseColor, Thickness - 3))
        using (SolidBrush INS = new SolidBrush(InsideColor))
        {
            if (FillInside)
            {
                G.FillEllipse(INS, new Rectangle(33, 33, Width - 66, Height - 66));
            }
            if (ShowBase)
            {
                G.DrawArc(BG, PGA, -90, 360);
            }

        }

        using (Pen PC = new Pen(ProgressColor, Thickness - 3) { StartCap = StartStyle, EndCap = EndStyle })
        using (SolidBrush PT = new SolidBrush(ProgressTextColor))
        {
            if (CurrentValue != 0)
            {
                G.DrawArc(PC, PGA, -90, CurrentValue);
            }
            if (ShowProgressValue)
            {
                G.DrawString(Value + "%", Font, PT, Rect, H.SetPosition());
            }
        }


    }

    #endregion

}

#endregion

#endregion

#region GroupBox 

public class DelightGroupBox : ContainerControl
{

    #region Variables 

    private Color _HeaderTextColor = Colors.GroupText;
    private Color _BorderColor = Colors.GroupBorder;
    private Color _BaseColor = Colors.GroupBase;
    private int _Header = 50;

    #endregion

    #region Properties 

    [Category("Custom Properties"), Description("Gets or sets the text color for the control.")]
    public Color HeaderTextColor
    {
        get { return _HeaderTextColor; }
        set
        {
            _HeaderTextColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the background border color for the control.")]
    public Color BorderColor
    {
        get { return _BorderColor; }
        set
        {
            _BorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the background color for the control.")]
    public Color BaseColor
    {
        get { return _BaseColor; }
        set
        {
            _BaseColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the size of the header.")]
    public int Header
    {
        get { return _Header; }
        set
        {
            _Header = value;
            Invalidate();
        }
    }

    #endregion

    #region Draw Control 

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;
        Rectangle Rect = new Rectangle(0, 0, Width, Height);

        using (SolidBrush B = new SolidBrush(BaseColor))
        using (Pen P = new Pen(BorderColor))
        using (SolidBrush TB = new SolidBrush(HeaderTextColor))
        using (StringFormat SF = new StringFormat { LineAlignment = StringAlignment.Center })
        {
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            G.FillRectangle(B, Rect);
            G.DrawLine(P, new Point(0, Header), new Point(Width, Header));
            G.DrawString(Text, Font, TB, new Rectangle(5, 0, Width, Header), SF);
        }

    }

    #endregion

    #region Constructors 

    public DelightGroupBox()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        UpdateStyles();
        Font = new Font("Segoe UI", 10);
        BackColor = Color.Transparent;
    }

    #endregion

}

#endregion

#region ListBox

[DefaultEvent("SelectedIndexChanged"), DefaultProperty("Items")]
public class DelightListBox : Control
{

    #region Declarations

    private static readonly HelperMethods H = new HelperMethods();
    private List<string> _Items = new List<string>();
    private readonly List<string> _SelectedItems = new List<string>();
    private bool _MultiSelect = true;
    private int _ItemHeight = 25;
    private Color _BaseColor = Colors.InputBase;
    private Color _BorderColor = Colors.InputBorder;
    private Color _UnSelectedItemesColor = Colors.InputBorder;
    private Color _SelectedItemesColor = Colors.InputBase;
    private Color _SelectedItemesBackColor = Colors.InputSelected;
    private Color _SelectedItemesBorderColor = Colors.InputSelected;
    private DelightVerticalScrollBar SVS = new DelightVerticalScrollBar();
    private int _SelectedIndex;
    private string _SelectedItem;
    private bool _ShowScrollBar = false;
    private Point _MouseLocation = new Point(-1, -1);
    private int _RoundRadius = 0;

    #endregion

    #region Properties

    [TypeConverter(typeof(CollectionConverter)),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Custom Properties"),
     Description("Gets the items of ListBox.")]
    public string[] Items
    {
        get { return _Items.ToArray(); }
        set
        {
            _Items = new List<string>(value);
            InvalidateScroll();
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets a collection containing the currently selected items in ListBox.")
    ]
    public string[] SelectedItems
    {
        get { return _SelectedItems.ToArray(); }
    }

    [Category("Custom Properties"), Description("Gets or sets the height of an item in ListBox.")]
    public int ItemHeight
    {
        get { return _ItemHeight; }
        set
        {
            _ItemHeight = value;
            Invalidate();
        }
    }

    [Browsable(false), Category("Custom Properties"),
     Description("Gets or sets the currently selected item in ListBox.")]
    public string SelectedItem
    {
        get { return _SelectedItem; }
        set
        {
            _SelectedItem = value;
            Invalidate();
        }
    }

    [Browsable(false), Category("Custom Properties"),
     Description("Gets or sets the zero-based index of the currently selected item in ListBox.")]
    public int SelectedIndex
    {
        get { return _SelectedIndex; }
        set
        {
            _SelectedIndex = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"),
     Description("Gets or sets a value indicating whether ListBox supports multiple rows.")]
    public bool MultiSelect
    {
        get { return _MultiSelect; }
        set
        {
            _MultiSelect = value;

            if (_SelectedItems.Count > 1)
                _SelectedItems.RemoveRange(1, _SelectedItems.Count - 1);

            Invalidate();
        }
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public int Count
    {
        get { return _Items.Count; }
    }

    [Category("Custom Properties"), Description("Gets or sets the background color of the control.")]
    public Color BaseColor
    {
        get { return _BaseColor; }
        set
        {
            _BaseColor = value;
            SVS.BackColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the border color of the control.")]
    public Color BorderColor
    {
        get { return _BorderColor; }
        set
        {
            _BorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the un-selected itemes color of the control.")]
    public Color UnSelectedItemesColor
    {
        get { return _UnSelectedItemesColor; }
        set
        {
            _UnSelectedItemesColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the selected itemes color of the control.")]
    public Color SelectedItemesColor
    {
        get { return _SelectedItemesColor; }
        set
        {
            _SelectedItemesColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the selected itemes background color of the control.")]
    public Color SelectedItemesBackColor
    {
        get { return _SelectedItemesBackColor; }
        set
        {
            _SelectedItemesBackColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"),
     Description("Gets or sets the selected itemes background border color of the control.")]
    public Color SelectedItemesBorderColor
    {
        get { return _SelectedItemesBorderColor; }
        set
        {
            _SelectedItemesBorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"),
     Description("Gets or sets a value indicating whether the vertical scroll bar is shown or not.")]
    public bool ShowScrollBar
    {
        get { return _ShowScrollBar; }
        set
        {
            if (value)
            {
                if (!Controls.Contains(SVS))
                {
                    Controls.Add(SVS);
                }
            }
            else
            {
                if (Controls.Contains(SVS))
                {
                    Controls.Remove(SVS);
                }
            }
            _ShowScrollBar = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether the control can Rounded in corners.")]
    public int RoundRadius
    {
        get { return _RoundRadius; }
        set
        {
            _RoundRadius = value;
            Invalidate();
        }
    }

    #endregion

    #region Events

    public event SelectedIndexChangedEventHandler SelectedIndexChanged;
    public delegate void SelectedIndexChangedEventHandler(object sender);

    public void AddItem(string newItem)
    {
        _Items.Add(newItem);
        Invalidate();
        InvalidateScroll();
    }

    public void AddItems(string[] newItems)
    {
        _Items.AddRange(newItems);
        Invalidate();
        InvalidateScroll();
    }

    public void RemoveItemAt(int index)
    {
        _Items.RemoveAt(index);
        Invalidate();
        InvalidateScroll();
    }

    public void RemoveItem(string item)
    {
        _Items.Remove(item);
        Invalidate();
        InvalidateScroll();
    }

    public int IndexOf(string value)
    {
        return _Items.IndexOf(value);
    }

    public bool Contains(object item)
    {
        return _Items.Contains(item.ToString());
    }

    public void RemoveItems(string[] itemsToRemove)
    {
        foreach (string item in itemsToRemove)
        {
            _Items.Remove(item);
        }
        Invalidate();
        InvalidateScroll();
    }

    public void Clear()
    {
        for (int i = _Items.Count - 1; i >= 0; i += -1)
        {
            _Items.RemoveAt(i);
        }
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        InvalidateScroll();
        InvalidateLayout();
        base.OnSizeChanged(e);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (!MultiSelect && keyData == Keys.Down | keyData == Keys.Next)
        {
            _SelectedItems.Remove(SelectedItem);
            SelectedIndex = SelectedIndex + 1;
            _SelectedItems.Add(_Items[SelectedIndex]);
            SelectedIndexChanged?.Invoke(this);
            SelectedItem = _Items[SelectedIndex];
            Invalidate();
            return true;
        }
        else if (!MultiSelect && keyData == Keys.Up | keyData == Keys.Back)
        {
            _SelectedItems.Remove(SelectedItem);
            SelectedIndex = SelectedIndex - 1;
            _SelectedItems.Add(_Items[SelectedIndex]);
            SelectedIndexChanged?.Invoke(this);
            SelectedItem = _Items[SelectedIndex];
            Invalidate();
            return true;
        }
        else
        {
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        Focus();
        if (e.Button == MouseButtons.Left)
        {
            int offset = Convert.ToInt32(SVS.Value * (SVS.Maximum + (Height - (ItemHeight))));
            int index = (e.Y + offset) / ItemHeight;
            if (index > Items.Length - 1)
                index = -1;
            if (index != -1)
            {
                if (ModifierKeys == Keys.Control && MultiSelect)
                {
                    if (_SelectedItems.Contains(_Items[index]))
                    {
                        _SelectedItems.Remove(_Items[index]);
                    }
                    else
                    {
                        _SelectedItems.Add(_Items[index]);
                    }
                }
                else
                {
                    _SelectedItems.Clear();
                    _SelectedItems.Add(_Items[index]);
                    SelectedIndex = index;
                    SelectedIndexChanged?.Invoke(this);
                    SelectedItem = _Items[index];
                }
            }
            Invalidate();
        }
        base.OnMouseDown(e);
    }

    private void HandleScroll(object sender)
    {
        Invalidate();
    }

    private void InvalidateScroll()
    {
        if (Convert.ToInt32(Math.Round(((double)(Items.Length) * ItemHeight) / 1)) <
            Convert.ToDouble((((Items.Length) * ItemHeight) / 1)))
        {
            SVS.Maximum = Convert.ToInt32(Math.Ceiling(((double)(Items.Length) * ItemHeight) / 1));
        }
        else if (Convert.ToInt32(Math.Round(((double)(Items.Length) * ItemHeight) / 1)) == 0)
        {
            SVS.Maximum = 1;
        }
        else
        {
            SVS.Maximum = Convert.ToInt32(Math.Round(((double)(Items.Length) * ItemHeight) / 1));
        }
        Invalidate();
    }

    private void VS_MouseDown(object sender, MouseEventArgs e)
    {
        Focus();
    }

    private void InvalidateLayout()
    {
        SVS.Location = new Point(Width - SVS.Width - 1, 1);
        SVS.Size = new Size(18, Height - 3);
        Invalidate();
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
        int Move = -((e.Delta * SystemInformation.MouseWheelScrollLines / 120) * (2 / 2));
        int Value = Math.Max(Math.Min(SVS.Value + Move, SVS.Maximum), SVS.Minimum);
        SVS.Value = Value;
        base.OnMouseWheel(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        _MouseLocation = e.Location;
        Invalidate();
    }

    #endregion

    #region Constructors

    public DelightListBox()
    {
        SetStyle(
            ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
            ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        Font = new Font("Segoe UI", 8, FontStyle.Regular);
        BackColor = Color.Transparent;
        Size = new Size(130, 100);
        SVS.Scroll += HandleScroll;
        SVS.MouseDown += VS_MouseDown;
        SVS.BackColor = BaseColor;
        SVS.SmallChange = ItemHeight;
        SVS.LargeChange = ItemHeight * 2;
    }

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        GraphicsPath GP = new GraphicsPath();
        GraphicsPath GP2 = new GraphicsPath();
        Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);

        if (RoundRadius > 0)
        {
            G.SmoothingMode = SmoothingMode.AntiAlias;
            GP = H.RoundRec(Rect, RoundRadius);
        }
        else
        {
            GP.AddRectangle(Rect);
        }

        GP.CloseFigure();

        using (SolidBrush BG = new SolidBrush(BaseColor))
        using (SolidBrush USIC = new SolidBrush(UnSelectedItemesColor))
        using (SolidBrush SIC = new SolidBrush(SelectedItemesColor))
        using (SolidBrush SIBC = new SolidBrush(SelectedItemesBackColor))
        using (Pen SIBRC = new Pen(SelectedItemesBorderColor))
        {
            G.FillPath(BG, GP);

            int offset = Convert.ToInt32(SVS.Value * (SVS.Maximum + (Height - (ItemHeight))));
            int startIndex = 0;
            if (offset == 0)
                startIndex = 0;
            else
                startIndex = Convert.ToInt32(offset / ItemHeight / SVS.Maximum);

            for (int i = startIndex; i <= _Items.Count - 1; i++)
            {
                string currentItem = Items[i];
                int Y = ((i * ItemHeight) + 1 - offset) + Convert.ToInt32((ItemHeight / 2) - 8);

                Rectangle RR = new Rectangle(0, (i * ItemHeight) + 1 - offset, Width - 2, ItemHeight - 1);
                GP2 = new GraphicsPath();
                if (RoundRadius > 0)
                {
                    GP2 = H.RoundRec(RR, RoundRadius);
                }
                else
                {
                    GP2.AddRectangle(RR);
                }

                if (RR.Contains(_MouseLocation))
                {
                    G.FillPath(SIC, GP2);
                    G.DrawString(currentItem, Font, USIC, new Rectangle(5, Y, Width - 34, Y + 10));
                }
                else
                {
                    G.FillPath(BG, GP2);
                    G.DrawString(currentItem, Font, USIC, new Rectangle(5, Y, Width - 34, Y + 10));
                }

                if (_SelectedItems.Contains(currentItem))
                {
                    G.FillPath(SIBC, GP2);
                    G.DrawPath(SIBRC, GP2);
                    G.DrawString(currentItem, Font, SIC, new Rectangle(5, Y, Width - 34, Y + 10));
                }
            }

        }

        using (Pen borderPen = new Pen(BorderColor))
        {
            G.DrawPath(borderPen, GP);
        }
        GP.Dispose();
        GP2.Dispose();
    }

    #endregion

}

#endregion

#region ScrollBars

#region Vertical ScrollBars

[DefaultEvent("Scroll"), DefaultProperty("Value")]
public class DelightVerticalScrollBar : Control
{

    #region Declarations

    private static readonly HelperMethods H = new HelperMethods();
    private Color _BackColor = Colors.ScrollbarBase;
    private Color _ThumbColor = Colors.ScrollbarThumb;
    private Color _ArrowColor = Colors.ScrollbarArrow;
    private int _Minimum = 0;
    private int _Maximum = 100;
    private int _Value = 0;
    private int _SmallChange = 1;
    private int II;
    private int _LargeChange = 10;
    private Point MouseLocation = new Point(1, 1);
    private HelperMethods.MouseMode _ThumbState = HelperMethods.MouseMode.Normal;
    private HelperMethods.MouseMode _ArrowState = HelperMethods.MouseMode.Normal;
    private Rectangle UpperArrow;
    private Rectangle LowerArrow;
    private Rectangle Shaft;
    private Rectangle Thumb;
    private bool ShowThumb;
    private int _ThumbSize = 20;

    #endregion

    #region Properties

    [Category("Custom Properties"), Description("Gets or sets the background color for the control.")]
    public new Color BackColor
    {
        get { return _BackColor; }
        set
        {
            _BackColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the thumb color for the control.")]
    public Color ThumbColor
    {
        get { return _ThumbColor; }
        set
        {
            _ThumbColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the arrows color for the control.")]
    public Color ArrowColor
    {
        get { return _ArrowColor; }
        set
        {
            _ArrowColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the lower limit of the scrollable range.")]
    public int Minimum
    {
        get { return _Minimum; }
        set
        {
            _Minimum = value;
            if (value > _Value)
            {
                _Value = value;
            }
            else if (value > _Maximum)
            {
                _Maximum = value;
            }
            InvalidateLayout();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the upper limit of the scrollable range.")]
    public int Maximum
    {
        get { return _Maximum; }
        set
        {
            if (value < _Value)
            {
                _Value = value;
            }
            else if (value < _Minimum)
            {
                _Minimum = value;
            }
            InvalidateLayout();
        }
    }

    [Category("Custom Properties"),
     Description("Gets or sets a numeric value that represents the current position of the scroll bar box.")]
    public int Value
    {
        get { return _Value; }
        set
        {
            if (value > _Maximum)
            {
                throw new Exception("Already reached to the maximum value.");
            }
            else if (value < _Minimum)
            {
                throw new Exception("Already reached to the minimum value.");
            }
            else
            {
                _Value = value;
            }
            InvalidatePosition();
            Scroll?.Invoke(this);
        }
    }

    [Category("Custom Properties"),
     Description("Gets or sets the distance to move a scroll bar in response to a small scroll command.")]
    public int SmallChange
    {
        get { return _SmallChange; }
        set
        {
            _SmallChange = value;
            Invalidate();

        }
    }

    [Category("Custom Properties"),
     Description("Gets or sets the distance to move a scroll bar in response to a large scroll command.")]
    public int LargeChange
    {
        get { return _LargeChange; }
        set
        {
            _LargeChange = value;
            Invalidate();
        }
    }

    #endregion

    #region Events

    protected override void OnSizeChanged(EventArgs e)
    {
        InvalidateLayout();
    }

    private void InvalidateLayout()
    {

        UpperArrow = new Rectangle(0, 0, Width, 0x10);
        Shaft = new Rectangle(0, UpperArrow.Bottom + 1, Width,
            Convert.ToInt32((Height - (((double)Height) / 8.0)) - 8.0));
        ShowThumb = (Maximum - Minimum) > 0;
        if (ShowThumb)
        {
            Thumb = new Rectangle(4, 0, Width - 8, Convert.ToInt32(((double)Height) / 8.0));
        }
        Scroll?.Invoke(this);
        InvalidatePosition();
    }

    public event ScrollEventHandler Scroll;

    public delegate void ScrollEventHandler(object sender);

    public void InvalidatePosition()
    {
        Thumb.Y = Convert.ToInt32((double)(Value - Minimum) / (Maximum - Minimum) * (Shaft.Height - _ThumbSize) + 16.0);
        Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Button == MouseButtons.Left && ShowThumb)
        {
            if (UpperArrow.Contains(e.Location))
            {
                _ArrowState = HelperMethods.MouseMode.Pushed;
                II = Value - SmallChange;
            }
            else if (LowerArrow.Contains(e.Location))
            {
                II = Value + SmallChange;
                _ArrowState = HelperMethods.MouseMode.Pushed;
            }
            else
            {
                if (Thumb.Contains(e.Location))
                {
                    _ThumbState = HelperMethods.MouseMode.Pushed;
                    Invalidate();
                    return;
                }
                if (e.Y < Thumb.Y)
                {
                    II = Value - LargeChange;
                }
                else
                {
                    II = Value + LargeChange;
                }
            }
            Value = Math.Min(Math.Max(II, Minimum), Maximum);
            Invalidate();
            InvalidatePosition();
        }

    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (_ThumbState == HelperMethods.MouseMode.Pushed | _ArrowState == HelperMethods.MouseMode.Pushed && ShowThumb)
        {
            int thumbPosition = (e.Y - UpperArrow.Height) - (_ThumbSize / 2);
            int thumbBounds = Shaft.Height - _ThumbSize;
            II = Convert.ToInt32(((double)(thumbPosition) / thumbBounds) * (Maximum - Minimum)) - Minimum;
            Value = Math.Min(Math.Max(II, Minimum), Maximum);
            InvalidatePosition();
        }

    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (Thumb.Contains(e.Location))
        {
            _ThumbState = HelperMethods.MouseMode.Hovered;
        }
        else
        {
            _ThumbState = HelperMethods.MouseMode.Normal;
        }
        if ((e.Location.Y < 0x10) | (e.Location.Y > (Width - 0x10)))
        {
            _ThumbState = HelperMethods.MouseMode.Hovered;
        }
        else
        {
            _ThumbState = HelperMethods.MouseMode.Normal;
        }
        Invalidate();


    }

    protected override void OnMouseLeave(EventArgs e)
    {
        _ThumbState = HelperMethods.MouseMode.Normal;
        _ArrowState = HelperMethods.MouseMode.Normal;
        Invalidate();
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        Invalidate();
    }

    #endregion

    #region Contsructors

    public DelightVerticalScrollBar()
    {
        SetStyle(
            ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw |
            ControlStyles.UserPaint | ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        Size = new Size(19, 50);
        UpdateStyles();
    }

    #endregion

    #region Draw Control

    protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
    {
        Graphics G = e.Graphics;

        G.SmoothingMode = SmoothingMode.HighQuality;
        G.PixelOffsetMode = PixelOffsetMode.HighQuality;
        G.Clear(BackColor);
        using (SolidBrush ThumbBrush = new SolidBrush(ThumbColor))
        {
            G.FillRectangle(ThumbBrush, Thumb);
        }

        Point[] UpperTriangle = H.Triangle(new Point(Convert.ToInt32(Width / 2), 5),
            new Point(Convert.ToInt32(Width / 4), 11), new Point(Convert.ToInt32(Width / 2 + Width / 4), 11));
        Point[] LowerTriangle = H.Triangle(new Point(Convert.ToInt32(Width / 2), Height - 5),
            new Point(Convert.ToInt32(Width / 4), Height - 11), new Point(Convert.ToInt32(Width / 2 + Width / 4), Height - 11));

        using (SolidBrush ArrowBrush = new SolidBrush(ArrowColor))
        {
            G.FillPolygon(ArrowBrush, UpperTriangle);
            G.FillPolygon(ArrowBrush, LowerTriangle);
        }

    }

    #endregion

}

#endregion

#region Horizontal ScrollBars

[DefaultEvent("Scroll"), DefaultProperty("Value")]
public class DelightHorizontalScrollBar : Control
{

    #region Declarations

    private static readonly HelperMethods H = new HelperMethods();
    private Color _BackColor = Colors.ScrollbarBase;
    private Color _ThumbColor = Colors.ScrollbarThumb;
    private Color _ArrowColor = Colors.ScrollbarArrow;
    private int _Minimum = 0;
    private int _Maximum = 100;
    private int _Value = 0;
    private int _SmallChange = 1;
    private int _LargeChange = 10;
    private Point MouseLocation = new Point(1, 1);
    private HelperMethods.MouseMode _ThumbState = HelperMethods.MouseMode.Normal;
    private HelperMethods.MouseMode _ArrowState = HelperMethods.MouseMode.Normal;
    private int II;
    private Rectangle UpperArrow;
    private Rectangle LowerArrow;
    private Rectangle Shaft;
    private Rectangle Thumb;
    private bool ShowThumb;
    private int _ThumbSize = 25;

    #endregion

    #region Properties

    [Category("Custom Properties"), Description("Gets or sets the background color for the control.")]
    public new Color BackColor
    {
        get { return _BackColor; }
        set
        {
            _BackColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the thumb color for the control.")]
    public Color ThumbColor
    {
        get { return _ThumbColor; }
        set
        {
            _ThumbColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the arrows color for the control.")]
    public Color ArrowColor
    {
        get { return _ArrowColor; }
        set
        {
            _ArrowColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the lower limit of the scrollable range.")]
    public int Minimum
    {
        get { return _Minimum; }
        set
        {
            _Minimum = value;
            if (value > _Value)
            {
                _Value = value;
            }
            else if (value > _Maximum)
            {
                _Maximum = value;
            }
            InvalidateLayout();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the upper limit of the scrollable range.")]
    public int Maximum
    {
        get { return _Maximum; }
        set
        {
            if (value < _Value)
            {
                _Value = value;
            }
            else if (value < _Minimum)
            {
                _Minimum = value;
            }
            InvalidateLayout();
        }
    }

    [Category("Custom Properties"),
     Description("Gets or sets a numeric value that represents the current position of the scroll bar box.")]
    public int Value
    {
        get { return _Value; }
        set
        {
            if (value > _Maximum)
            {
                throw new Exception("Already reached to the maximum value.");
            }
            else if (value < _Minimum)
            {
                throw new Exception("Already reached to the minimum value.");
            }
            else
            {
                _Value = value;
            }
            InvalidatePosition();
            Scroll?.Invoke(this);
        }
    }

    [Category("Custom Properties"),
     Description("Gets or sets the distance to move a scroll bar in response to a small scroll command.")]
    public int SmallChange
    {
        get { return _SmallChange; }
        set
        {
            _SmallChange = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"),
     Description("Gets or sets the distance to move a scroll bar in response to a large scroll command.")]
    public int LargeChange
    {
        get { return _LargeChange; }
        set
        {
            _LargeChange = value;
            Invalidate();
        }
    }

    #endregion

    #region Events

    protected override void OnSizeChanged(EventArgs e)
    {
        InvalidateLayout();
    }

    private void InvalidateLayout()
    {
        UpperArrow = new Rectangle(0, 0, 16, Height);
        Shaft = new Rectangle(UpperArrow.Right + 1, 0, Convert.ToInt32(Width - Width / 8 - 8), Height);
        ShowThumb = Convert.ToBoolean(((_Maximum - _Minimum)));
        if (ShowThumb)
            Thumb = new Rectangle(0, 4, Convert.ToInt32(Width / 8), Height - 8);
        Scroll?.Invoke(this);
        InvalidatePosition();
    }

    public event ScrollEventHandler Scroll;

    public delegate void ScrollEventHandler(object sender);

    private void InvalidatePosition()
    {
        Thumb.X =
            Convert.ToInt32(

                ((((double)(_Value - _Minimum)) / ((double)(_Maximum - _Minimum))) * (Shaft.Width - _ThumbSize)) + 16.0);
        Invalidate();

    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        if ((e.Button == MouseButtons.Left) && ShowThumb)
        {
            if (UpperArrow.Contains(e.Location))
            {
                _ArrowState = HelperMethods.MouseMode.Pushed;
                II = _Value - _SmallChange;
            }
            else if (LowerArrow.Contains(e.Location))
            {
                II = _Value + _SmallChange;
                _ArrowState = HelperMethods.MouseMode.Pushed;
            }
            else
            {
                if (Thumb.Contains(e.Location))
                {
                    _ThumbState = HelperMethods.MouseMode.Pushed;
                    Invalidate();
                    return;
                }
                if (e.X < Thumb.X)
                {
                    II = _Value - _LargeChange;
                }
                else
                {
                    II = _Value + _LargeChange;
                }
            }
            Value = Math.Min(Math.Max(II, _Minimum), _Maximum);
            Invalidate();
            InvalidatePosition();
        }

    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        MouseLocation = e.Location;
        if (UpperArrow.Contains(MouseLocation))
        {
            _ArrowState = HelperMethods.MouseMode.Hovered;
        }
        else if (LowerArrow.Contains(MouseLocation))
        {
            _ArrowState = HelperMethods.MouseMode.Hovered;
        }
        else if (_ArrowState != HelperMethods.MouseMode.Pushed)
        {
            _ArrowState = HelperMethods.MouseMode.Normal;
        }
        if (Thumb.Contains(MouseLocation) & (_ThumbState != HelperMethods.MouseMode.Pushed))
        {
            _ThumbState = HelperMethods.MouseMode.Hovered;
        }
        else if (_ThumbState != HelperMethods.MouseMode.Pushed)
        {
            _ThumbState = HelperMethods.MouseMode.Normal;
        }
        Invalidate();
        if (_ThumbState == HelperMethods.MouseMode.Pushed | _ArrowState == HelperMethods.MouseMode.Pushed && ShowThumb)
        {
            int num = ((e.X + 2) - UpperArrow.Width) - (_ThumbSize / 2);
            II =
                Convert.ToInt32((((double)num) / ((double)Shaft.Width - _ThumbSize)) * (_Maximum - _Minimum)) -
                _Minimum;
            Value = Math.Min(Math.Max(II, _Minimum), _Maximum);
            InvalidatePosition();
        }

    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        MouseLocation = e.Location;
        if (Thumb.Contains(MouseLocation))
        {
            _ThumbState = HelperMethods.MouseMode.Hovered;
        }
        else if (!Thumb.Contains(MouseLocation))
        {
            _ThumbState = HelperMethods.MouseMode.Normal;
        }
        if (MouseLocation.X < 16 | MouseLocation.X > Width - 16)
        {
            _ThumbState = HelperMethods.MouseMode.Hovered;
        }
        else if (!(MouseLocation.X < 16) | MouseLocation.X > Width - 16)
        {
            _ThumbState = HelperMethods.MouseMode.Normal;
        }
        Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        _ThumbState = HelperMethods.MouseMode.Normal;
        _ArrowState = HelperMethods.MouseMode.Normal;
        Invalidate();
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        Invalidate();
    }

    #endregion

    #region Contsructors

    public DelightHorizontalScrollBar()
    {
        SetStyle(
            ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw |
            ControlStyles.UserPaint | ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        Size = new Size(50, 19);
    }

    #endregion

    #region Draw Control

    protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
    {
        Graphics G = e.Graphics;

        G.SmoothingMode = SmoothingMode.HighQuality;
        G.PixelOffsetMode = PixelOffsetMode.HighQuality;
        G.Clear(BackColor);
        using (SolidBrush ThumbBrush = new SolidBrush(ThumbColor))
        {
            G.FillRectangle(ThumbBrush, Thumb);
        }

        Point[] LeftTriangle = H.Triangle(new Point(5, Convert.ToInt32(Height / 2)),
            new Point(11, Convert.ToInt32(Height / 4)), new Point(11, Convert.ToInt32(Height / 2 + Height / 4)));

        Point[] RightTriangle = H.Triangle(new Point(Width - 5, Convert.ToInt32(Height / 2)),
            new Point(Width - 11, Convert.ToInt32(Height / 4)),
            new Point(Width - 11, Convert.ToInt32(Height / 2 + Height / 4)));

        using (SolidBrush ArrowBrush = new SolidBrush(ArrowColor))
        {
            G.FillPolygon(ArrowBrush, LeftTriangle);
            G.FillPolygon(ArrowBrush, RightTriangle);
        }


    }

    #endregion

}

#endregion

#endregion

#region ComboBox

[DefaultEvent("SelectedIndexChanged")]
public class DelightComboBox : ComboBox
{

    #region Declarations

    private static readonly HelperMethods H = new HelperMethods();
    private int _StartIndex = 0;
    private Color _BaseColor = Colors.InputBase;
    private Color _ShadowColor = Colors.InputShadow;
    private Color _BorderColor = Colors.InputBorder;
    private Color _ArrowColor = Colors.InputText;
    private Color _TextColor = Colors.InputText2;
    private Color _SelectedItemColor = Colors.InputBase;
    private Color _SelectedItemBackColor = Colors.InputSelected;
    private int _RoundRadius = 2;
    public new event SelectedIndexChangedEventHandler SelectedIndexChanged;
    public delegate void SelectedIndexChangedEventHandler(object sender);

    #endregion

    #region Constructors


    public DelightComboBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
        Font = new Font("Segoe UI", 11);
        DrawMode = DrawMode.OwnerDrawFixed;
        DoubleBuffered = true;
        StartIndex = 0;
        DropDownStyle = ComboBoxStyle.DropDownList;
        UpdateStyles();
    }

    #endregion

    #region Properties

    [Category("Custom Properties"), Description("Gets or sets the index specifying the currently selected item.")]
    private int StartIndex
    {
        get { return _StartIndex; }
        set
        {
            _StartIndex = value;
            try
            {
                base.SelectedIndex = value;
                SelectedIndexChanged?.Invoke(this);
            }
            catch
            {
            }
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the background color of the control.")]
    public Color BaseColor
    {
        get { return _BaseColor; }
        set
        {
            _BaseColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the inner shadow color of the control.")]
    public Color ShadowColor
    {
        get { return _ShadowColor; }
        set
        {
            _ShadowColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the border color of the control.")]
    public Color BorderColor
    {
        get { return _BorderColor; }
        set
        {
            _BorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the arrow handler color of the control.")]
    public Color ArrowColor
    {
        get { return _ArrowColor; }
        set
        {
            _ArrowColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the text color of the control.")]
    public Color TextColor
    {
        get { return _TextColor; }
        set
        {
            _TextColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the selected text color of the control.")]
    public Color SelectedItemColor
    {
        get { return _SelectedItemColor; }
        set
        {
            _SelectedItemColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the background color of the selected item of the control.")]
    public Color SelectedItemBackColor
    {
        get { return _SelectedItemBackColor; }
        set
        {
            _SelectedItemBackColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether the control can Rounded in corners.")]
    public int RoundRadius
    {
        get { return _RoundRadius; }
        set
        {
            _RoundRadius = value;
            Invalidate();
        }
    }


    #endregion

    #region Draw Control

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        Graphics G = e.Graphics;
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        try
        {
            using (SolidBrush BG = new SolidBrush((e.State & DrawItemState.Selected) == DrawItemState.Selected ? SelectedItemBackColor : BaseColor))
            using (SolidBrush TC = new SolidBrush((e.State & DrawItemState.Selected) == DrawItemState.Selected ? SelectedItemColor : TextColor))
            {
                G.FillRectangle(BG, e.Bounds);
                G.DrawString(GetItemText(Items[e.Index]), Font, TC, e.Bounds);
            }

        }
        catch
        {
        }

    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;

        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        GraphicsPath GP = new GraphicsPath();
        Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);

        if (RoundRadius > 0)
        {
            G.SmoothingMode = SmoothingMode.AntiAlias;
            GP = H.RoundRec(Rect, RoundRadius);
        }
        else
        {
            GP.AddRectangle(Rect);
        }

        GP.CloseFigure();
        using (PathGradientBrush B = new PathGradientBrush(GP) { CenterColor = BaseColor, SurroundColors = new Color[] { ShadowColor }, FocusScales = new PointF(0.98f, 0.75f) })
        using (Pen P = new Pen(BorderColor))
        {
            G.FillPath(B, GP);
            G.DrawPath(P, GP);
        }


        G.SmoothingMode = SmoothingMode.AntiAlias;

        H.DrawTriangle(G, ArrowColor, 2, new Point(Width - 20, 12), new Point(Width - 16, 16), new Point(Width - 16, 16), new Point(Width - 12, 12), new Point(Width - 16, 17), new Point(Width - 16, 16));
        G.SmoothingMode = SmoothingMode.None;
        using (SolidBrush TC = new SolidBrush(TextColor))
        {
            G.DrawString(Text, Font, TC, new Rectangle(7, 1, Width - 1, Height - 1), H.SetPosition(StringAlignment.Near));
        }
        GP.Dispose();
    }

    #endregion

    #region Events

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Invalidate();
    }

    #endregion

}

#endregion

#region Range

[DefaultEvent("RangeChanged")]
public class DelightRange : Control
{

    #region Declarations

    private static readonly HelperMethods H = new HelperMethods();
    private int _MinRange;
    private int _MaxRange;
    private int _MaximumRange = 100;
    private Rectangle RangeValue;
    private readonly CustomControl Track = new CustomControl();
    private readonly CustomControl Track2 = new CustomControl();
    private Color _BaseColor = Colors.SliderBase;
    private Color _RangeColor = Colors.SliderValue;
    private Color _RangeTextColor = Colors.SliderBase;
    public event RangeChangedEventHandler RangeChanged;
    public delegate void RangeChangedEventHandler(object sender);

    #endregion

    #region Properties

    [Category("Custom Properties"), Description("Gets or sets the upper limit of the range.")]
    public int MaximumRange
    {
        get { return _MaximumRange; }
        set
        {
            _MaximumRange = value;
            MaxRange = value * Track2.Left / (Width - 9);
            MinRange = value * Track.Left / (Width - 9);
            RenewCurrentValue();
            RangeChanged?.Invoke(this);
        }
    }

    [Category("Custom Properties"), Description("Gets the the value between minimum and maximum.")]
    public string SelectedRange
    {
        get { return string.Format("{0}-{1}", MinRange, MaxRange); }
    }
    
    [Category("Custom Properties"), Description("Gets or sets the upper limit value of the range.")]
    public int MaxRange
    {
        get { return _MaxRange; }
        set
        {
            if (value > MaximumRange)
                throw new Exception("Maximum Value Reached");
            _MaxRange = value;
            Track2.Left = (Width - 9) * _MaxRange / MaximumRange;
            if (Track2.Left < Track.Left)
            {
                Track2.Left = Track2.Left;
                throw new Exception("Maximum Value Reached");
            }
            RenewCurrentValue();
            RangeChanged?.Invoke(this);
            Track2.Text = value.ToString();
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the lower limit value of the range.")]
    public int MinRange
    {
        get { return _MinRange; }
        set
        {
            if (value > _MaximumRange)
                throw new Exception("Minmium Value Reached");
            _MinRange = value;
            Track.Left = (Width - 9) * _MinRange / _MaximumRange;
            if (Track.Left > Track2.Left)
            {
                Track.Left = Track.Left;
                throw new Exception("Minmium Value Reached");
            }
            RenewCurrentValue();
            RangeChanged?.Invoke(this);
            Track.Text = value.ToString();
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the control color.")]
    public Color BaseColor
    {
        get { return _BaseColor; }
        set
        {
            _BaseColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the range and range handler color.")]
    public Color RangeColor
    {
        get { return _RangeColor; }
        set
        {
            _RangeColor = value;
            Track.BaseColor = value;
            Track2.BaseColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the range text color.")]
    public Color RangeTextColor
    {
        get { return _RangeTextColor; }
        set
        {
            _RangeTextColor = value;
            Track.TextColor = value;
            Track2.TextColor = value;
            Invalidate();
        }
    }

    #endregion

    #region Constructors

    public DelightRange()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        SetDefaults();
        BackColor = Color.Transparent;
        Size = new Size(300, 20);
        MaxRange = MaximumRange * Track2.Left / (Width - 9);
        MinRange = MaximumRange * Track.Left / (Width - 9);
        UpdateStyles();
        RangeValue = new Rectangle(MinRange, 7, MaxRange, 4);
        RenewCurrentValue();
        Font = new Font("Segoe UI", 5);
    }

    public void SetDefaults()
    {
        Track.MouseMove += Track_MouseMove;
        Track.Size = new Size(8, 17);
        Track.Location = new Point(0, 0);
        Track.TabIndex = 1;
        Track.Text = MinRange.ToString();
        Track.BaseColor = Colors.SliderThumb;
        Track.BorderColor = Colors.SliderThumbBorder;
        Track.TextColor = Colors.SliderBase;
        Track2.MouseMove += Track2_MouseMove;
        Track2.Size = new Size(8, 17);
        Track2.Location = new Point(195, 0);
        Track2.TabIndex = 2;
        Track2.Text = MaxRange.ToString();
        Track2.BaseColor = Colors.SliderThumb;
        Track2.BorderColor = Colors.SliderThumbBorder;
        Track2.TextColor = Colors.SliderBase;
    }

    #endregion

    #region Events

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Height = 21;
    }

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        Controls.Add(Track);
        Controls.Add(Track2);
        Invalidate();
    }

    private void Track2_MouseMove(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            if (e.X + Track2.Right != MaxRange)
            {
                int num = e.X + Track2.Left;
                if (num > Track.Left && num + Track2.Width - 1 < Width)
                {
                    Track2.Left = num;
                    RenewCurrentValue();
                    MaxRange = (int)Math.Round(MaximumRange * Track2.Left / (double)(Width - 9));
                }
            }
        }

    }

    private void Track_MouseMove(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            if (e.X + Track.Left != MinRange)
            {
                int num = e.X + Track.Left;
                if (num < Track2.Left && num > -1 && num + Track.Width < Width)
                {
                    Track.Left = num;
                    RenewCurrentValue();
                    MinRange = (int)Math.Round(MaximumRange * Track.Left / (double)(Width - 9));
                }
            }
        }

    }

    public void RenewCurrentValue()
    {
        RangeValue.X = Track.Left + Track.Width / 2;
        RangeValue.Width = Track2.Left + Track2.Width / 2 - RangeValue.Left;
    }

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;
        using (SolidBrush BG = new SolidBrush(BaseColor))
        using (SolidBrush RC = new SolidBrush(RangeColor))
        using (SolidBrush RTC = new SolidBrush(RangeTextColor))
        {
            G.FillRectangle(BG, new Rectangle(0, 7, Width, 4));
            G.FillRectangle(RC, RangeValue);
            G.DrawString(MinRange.ToString(), Font, RTC, new Rectangle(Track.Left, Track.Top, Track.Width, Track.Height), H.SetPosition());
        }

    }


    #endregion

    #region Custom Control

    public sealed class CustomControl : Control
    {

        #region Declarations

        private static readonly HelperMethods H = new HelperMethods();
        private Color _BaseColor = H.GetHTMLColor("FF337AB7");
        private Color _BorderColor = H.GetHTMLColor("FF337AB7");
        private Color _TextColor = Color.White;

        #endregion

        #region Constructors

        public CustomControl()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
            UpdateStyles();
        }

        #endregion

        #region Properties

        public Color BaseColor
        {
            get { return _BaseColor; }
            set
            {
                _BaseColor = value;
                Invalidate();
            }
        }

        public Color BorderColor
        {
            get { return _BorderColor; }
            set
            {
                _BorderColor = value;
                Invalidate();
            }
        }

        public Color TextColor
        {
            get { return _TextColor; }
            set
            {
                _TextColor = value;
                Invalidate();
            }
        }

        #endregion

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            using (SolidBrush B = new SolidBrush(BaseColor))
            using (Pen P = new Pen(BorderColor))
            {
                Rectangle R = new Rectangle(1, 1, Width - 2, Height - 2);
                G.FillRectangle(B, R);
                G.DrawRectangle(P, R);
            }

        }

        #endregion

    }

    #endregion

}

#endregion

#region TextBox

[DefaultEvent("TextChanged")]
public class DelightTextbox : Control
{

    #region Declarations

    private TextBox _T = new TextBox();
    private TextBox T
    {
        get { return _T; }
        set
        {
            if (_T != null)
            {
                _T.MouseLeave -= T_MouseLeave;
                _T.MouseEnter -= T_MouseEnter;
                _T.MouseDown -= T_MouseEnter;
                _T.MouseHover -= T_MouseEnter;
                _T.TextChanged -= T_TextChanged;
                _T.KeyDown -= T_KeyDown;
            }
            _T = value;
            if (_T != null)
            {
                _T.MouseLeave += T_MouseLeave;
                _T.MouseEnter += T_MouseEnter;
                _T.MouseDown += T_MouseEnter;
                _T.MouseHover += T_MouseEnter;
                _T.TextChanged += T_TextChanged;
                _T.KeyDown += T_KeyDown;
            }
        }
    }
    private static readonly HelperMethods H = new HelperMethods();
    private HorizontalAlignment _TextAlign = HorizontalAlignment.Left;
    private int _MaxLength = 32767;
    private bool _ReadOnly = false;
    private bool _UseSystemPasswordChar = false;
    private string _WatermarkText = string.Empty;
    private Image _Image;
    private HelperMethods.MouseMode State = HelperMethods.MouseMode.Normal;
    private Color _ShadowColor = Colors.InputShadow;
    private Color _BaseColor = Colors.InputBase;
    private Color _ForeColor = Colors.InputBorder;
    private Color _HoverColor = Colors.InputSelected;
    private AutoCompleteSource _AutoCompleteSource = AutoCompleteSource.None;
    private AutoCompleteMode _AutoCompleteMode = AutoCompleteMode.None;
    private AutoCompleteStringCollection _AutoCompleteCustomSource;
    private bool _Multiline = false;
    private string[] _Lines = null;
    private int _RoundRadius = 0;


    #endregion

    #region Native Methods

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, string lParam);

    #endregion

    #region Properties

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public BorderStyle BorderStyle
    {
        get
        {
            return BorderStyle.None;
        }
    }

    [Category("Custom Properties"), Description("Gets or sets how text is aligned in TextBox control.")]
    public HorizontalAlignment TextAlign
    {
        get { return _TextAlign; }
        set
        {
            _TextAlign = value;
            if (T != null)
            {
                T.TextAlign = value;
            }
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets how text is aligned in TextBox control.")]
    public int MaxLength
    {
        get { return _MaxLength; }
        set
        {
            _MaxLength = value;
            if (T != null)
            {
                T.MaxLength = value;
            }
            Invalidate();
        }
    }

    [Browsable(false), ReadOnly(true)]
    public override Color BackColor
    {
        get { return Color.Transparent; }
    }

    [Category("Custom Properties"), Description("Gets or sets the background color of the control.")]
    public Color BaseColor
    {
        get { return _BaseColor; }
        set
        {
            _BaseColor = value;
            T.BackColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the inner shadow color of the control.")]
    public Color ShadowColor
    {
        get { return _ShadowColor; }
        set
        {
            _ShadowColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the color of the control whenever hovered.")]
    public Color HoverColor
    {
        get { return _HoverColor; }
        set
        {
            _HoverColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the foreground color of the control.")]
    public override Color ForeColor
    {
        get { return _ForeColor; }
        set
        {
            base.ForeColor = value;
            _ForeColor = value;
            T.ForeColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether text in the text box is read-only.")]
    public bool ReadOnly
    {
        get { return _ReadOnly; }
        set
        {
            _ReadOnly = value;
            if (T != null)
            {
                T.ReadOnly = value;
            }
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether the text in  TextBox control should appear as the default password character.")]
    public bool UseSystemPasswordChar
    {
        get { return _UseSystemPasswordChar; }
        set
        {
            _UseSystemPasswordChar = value;
            if (T != null)
            {
                T.UseSystemPasswordChar = value;
            }
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether this is a multiline System.Windows.Forms.TextBox control.")]
    public bool Multiline
    {
        get { return _Multiline; }
        set
        {
            _Multiline = value;
            if (T == null)
                return;
            T.Multiline = value;
            if (value)
            {
                T.Height = Height - 10;
            }
            else
            {
                Height = T.Height + 10;
            }
        }
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Image BackgroundImage
    {
        get { return null; }
    }

    [Category("Custom Properties"), Description("Gets or sets the current text in  TextBox.")]
    public override string Text
    {
        get { return T.Text; }
        set
        {
            base.Text = value;
            if (T != null)
            {
                T.Text = value;
            }
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the text in the System.Windows.Forms.TextBox while being empty.")]
    public string WatermarkText
    {
        get { return _WatermarkText; }
        set
        {
            _WatermarkText = value;
            SendMessage(T.Handle, 5377, 0, value);
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the image of the control.")]
    public Image Image
    {
        get { return _Image; }
        set
        {
            _Image = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a value specifying the source of complete strings used for automatic completion.")]
    public AutoCompleteSource AutoCompleteSource
    {
        get { return _AutoCompleteSource; }
        set
        {
            _AutoCompleteSource = value;
            if (T != null)
            {
                T.AutoCompleteSource = value;
            }
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a value specifying the source of complete strings used for automatic completion.")]
    public AutoCompleteStringCollection AutoCompleteCustomSource
    {
        get { return _AutoCompleteCustomSource; }
        set
        {
            _AutoCompleteCustomSource = value;
            if (T != null)
            {
                T.AutoCompleteCustomSource = value;
            }
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets an option that controls how automatic completion works for the TextBox.")]
    public AutoCompleteMode AutoCompleteMode
    {
        get { return _AutoCompleteMode; }
        set
        {
            _AutoCompleteMode = value;
            if (T != null)
            {
                T.AutoCompleteMode = value;
            }
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the font of the text displayed by the control.")]
    public new Font Font
    {
        get { return base.Font; }
        set
        {
            base.Font = value;
            if (T == null)
                return;
            T.Font = value;
            T.Location = new Point(5, 5);
            T.Width = Width - 8;
            if (!Multiline)
                Height = T.Height + 11;
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the lines of text in the control.")]
    public string[] Lines
    {
        get { return _Lines; }
        set
        {
            _Lines = value;
            if (T == null)
                return;
            T.Lines = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether the control can Rounded in corners.")]
    public int RoundRadius
    {
        get { return _RoundRadius; }
        set
        {
            _RoundRadius = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the ContextMenuStrip associated with this control.")]
    public override ContextMenuStrip ContextMenuStrip
    {
        get { return base.ContextMenuStrip; }
        set
        {
            base.ContextMenuStrip = value;
            if (T == null)
                return;
            T.ContextMenuStrip = value;
            Invalidate();
        }
    }

    #endregion

    #region Constructors

    public DelightTextbox()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        UpdateStyles();
        Font = new Font("Segoe UI", 10);
        T.Multiline = false;
        T.Cursor = Cursors.IBeam;
        T.BackColor = BaseColor;
        T.ForeColor = ForeColor;
        T.BorderStyle = BorderStyle.None;
        T.Location = new Point(7, 8);
        T.Font = Font;
        T.UseSystemPasswordChar = UseSystemPasswordChar;
        Size = new Size(135, 30);
        if (Multiline)
        {
            T.Height = Height - 11;
        }
        else
        {
            Height = T.Height + 11;
        }
    }

    #endregion

    #region Events

    public new event TextChangedEventHandler TextChanged;
    public delegate void TextChangedEventHandler(object sender);

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        if (!Controls.Contains(T))
            Controls.Add(T);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        T.Size = new Size(Width - 10, Height - 10);
    }

    #region TextBox MouseEvents

    private void T_MouseLeave(object sender, EventArgs e)
    {
        State = HelperMethods.MouseMode.Normal;
        Invalidate();
    }

    private void T_MouseEnter(object sender, EventArgs e)
    {
        State = HelperMethods.MouseMode.Pushed;
        Invalidate();
    }

    private void T_TextChanged(object sender, EventArgs e)
    {
        Text = T.Text;
        TextChanged?.Invoke(this);
        Invalidate();
    }

    private void T_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.A)
            e.SuppressKeyPress = true;
        if (e.Control && e.KeyCode == Keys.C)
        {
            T.Copy();
            e.SuppressKeyPress = true;
        }
        State = HelperMethods.MouseMode.Pushed;
        Invalidate();
    }

    #endregion

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        GraphicsPath GP = new GraphicsPath();
        Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);

        if (RoundRadius > 0)
        {
            G.SmoothingMode = SmoothingMode.AntiAlias;
            GP = H.RoundRec(Rect, RoundRadius);
        }
        else
        {
            GP.AddRectangle(Rect);
        }

        GP.CloseFigure();
        using (PathGradientBrush B = new PathGradientBrush(GP) { CenterColor = BaseColor, SurroundColors = new Color[] { ShadowColor }, FocusScales = new PointF(0.98f, 0.75f) })
        using (Pen P = new Pen(ForeColor))
        using (Pen P2 = new Pen(HoverColor))
        {
            switch (State)
            {
                case HelperMethods.MouseMode.Normal:
                    G.FillPath(B, GP);
                    G.DrawPath(P, GP);
                    break;
                case HelperMethods.MouseMode.Pushed:
                    G.FillPath(B, GP);
                    G.DrawPath(P2, GP);
                    break;
            }

        }

        if (Image != null)
        {
            T.Location = new Point(31, 5);
            T.Width = Width - 60;
            G.InterpolationMode = InterpolationMode.HighQualityBicubic;
            G.DrawImage(Image, new Rectangle(8, 6, 16, 16));
        }
        else
        {
            if (RoundRadius >= 20)
            {
                T.Location = new Point(10, 5);
                T.Width = Width - 20;
            }
            else
            {
                T.Location = new Point(7, 5);
                T.Width = Width - 10;
            }
        }

        GP.Dispose();

    }

    #endregion

}

#endregion

#region RichTextBox

[DefaultEvent("TextChanged")]
public class DelightRichTextbox : Control
{

    #region Declarations

    private RichTextBox _T = new RichTextBox();
    private RichTextBox T
    {
        get { return _T; }
        set
        {
            if (_T != null)
            {
                _T.SelectionChanged -= T_SelectionChanged;
                _T.SelectionChanged -= T_LinkClicked;
                _T.Protected -= T_Protected;
                _T.TextChanged -= T_TextChanged;
                _T.KeyDown -= T_KeyDown;
                _T.MouseHover -= T_MouseHover;
                _T.MouseLeave -= T_MouseLeave;
                _T.MouseUp -= T_MouseUp;
                _T.MouseEnter -= T_MouseEnter;
                _T.MouseDown -= T_MouseDown;
            }
            _T = value;
            if (_T != null)
            {
                _T.SelectionChanged += T_SelectionChanged;
                _T.SelectionChanged += T_LinkClicked;
                _T.Protected += T_Protected;
                _T.TextChanged += T_TextChanged;
                _T.KeyDown += T_KeyDown;
                _T.MouseHover += T_MouseHover;
                _T.MouseLeave += T_MouseLeave;
                _T.MouseUp += T_MouseUp;
                _T.MouseEnter += T_MouseEnter;
                _T.MouseDown += T_MouseDown;
            }
        }
    }

    private static readonly HelperMethods H = new HelperMethods();
    private bool _ReadOnly = false;
    private Color _BaseColor = Colors.InputBase;
    private Color _ShadowColor = Colors.InputShadow;
    private Color _ForeColor = Colors.InputBorder;
    //private HelperMethods.MouseMode State = HelperMethods.MouseMode.Normal;
    private bool _WordWrap;
    private bool _AutoWordSelection;
    private string[] _Lines = null;
    private int _RoundRadius = 0;
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]

    #endregion

    #region Native Methods

    private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
string lParam);

    #endregion

    #region Properties

    [Category("Custom Properties"), Description("Gets or sets the lines of text in the control.")]
    public string[] Lines
    {
        get { return _Lines; }
        set
        {
            _Lines = value;
            if (T == null)
                return;
            T.Lines = value;
            Invalidate();
        }
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public BorderStyle BorderStyle
    {
        get
        {
            return BorderStyle.None;
        }
    }

    [Browsable(false), ReadOnly(true)]
    public override Color BackColor
    {
        get { return Color.Transparent; }
    }

    [Category("Custom Properties"), Description("Gets or sets the background color of the control.")]
    public Color BaseColor
    {
        get { return _BaseColor; }
        set
        {
            T.BackColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the inner shadow color of the control.")]
    public Color ShadowColor
    {
        get { return _ShadowColor; }
        set
        {
            _ShadowColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the foreground color of the control.")]
    public new Color ForeColor
    {
        get { return _ForeColor; }
        set
        {
            base.ForeColor = value;
            _ForeColor = value;
            T.ForeColor = value;
            Invalidate();
        }
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Image[] BackgroungImage
    {
        get { return null; }
    }

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether text in the System.Windows.Forms.ToolStripTextBox is read-only.")]
    public bool ReadOnly
    {
        get { return _ReadOnly; }
        set
        {
            _ReadOnly = value;
            if (T != null)
            {
                T.ReadOnly = value;
            }
        }
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Multiline
    {
        get { return true; }
    }

    [Category("Custom Properties"), Description("Gets or sets the current text in  RichTextBox.")]
    public new string Text
    {
        get { return base.Text; }
        set
        {
            base.Text = value;
            if (T != null)
            {
                T.Text = value;
            }
        }
    }

    [Category("Custom Properties"), Description("Indicates whether a multiline text box control automatically wraps words to the beginning of the next line when necessary.")]
    public bool WordWrap
    {
        get { return _WordWrap; }
        set
        {
            _WordWrap = value;
            if (T != null)
            {
                T.WordWrap = value;
            }
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether automatic word selection is enabled.")]
    public bool AutoWordSelection
    {
        get { return _AutoWordSelection; }
        set
        {
            _AutoWordSelection = value;
            if (T != null)
            {
                T.AutoWordSelection = value;
            }
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether the control can Rounded in corners.")]
    public int RoundRadius
    {
        get { return _RoundRadius; }
        set
        {
            _RoundRadius = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the ContextMenuStrip associated with this control.")]
    public override ContextMenuStrip ContextMenuStrip
    {
        get { return base.ContextMenuStrip; }
        set
        {
            base.ContextMenuStrip = value;
            if (T == null)
                return;
            T.ContextMenuStrip = value;
            Invalidate();
        }
    }

    #endregion

    #region Constructors

    public DelightRichTextbox()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        WordWrap = true;
        AutoWordSelection = false;
        Font = new Font("Segoe UI", 10);
        T.Size = new Size(Width, Height);
        T.Multiline = true;
        T.Cursor = Cursors.IBeam;
        T.BackColor = BaseColor;
        T.ForeColor = ForeColor;
        T.BorderStyle = BorderStyle.None;
        T.Location = new Point(7, 5);
        T.Font = Font;
        UpdateStyles();
    }

    #endregion

    #region Events

    public new event TextChangedEventHandler TextChanged;
    public delegate void TextChangedEventHandler(object sender);
    public event SelectionChangedEventHandler SelectionChanged;
    public delegate void SelectionChangedEventHandler(object sender, System.EventArgs e);
    public event LinkClickedEventHandler LinkClicked;
    public delegate void LinkClickedEventHandler(object sender, EventArgs e);
    public event ProtectedEventHandler Protected;
    public delegate void ProtectedEventHandler(object sender, System.EventArgs e);

    private void T_SelectionChanged(object sender, System.EventArgs e)
    {
        SelectionChanged?.Invoke(sender, e);
    }

    private void T_LinkClicked(object sender, System.EventArgs e)
    {
        LinkClicked?.Invoke(sender, e);
    }

    private void T_Protected(object sender, System.EventArgs e)
    {
        Protected?.Invoke(sender, e);
    }

    private void T_TextChanged(object sender, EventArgs e)
    {
        Text = T.Text;
        TextChanged?.Invoke(this);
        Invalidate();
    }

    private void T_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.A)
            e.SuppressKeyPress = true;
        if (e.Control && e.KeyCode == Keys.C)
        {
            T.Copy();
            e.SuppressKeyPress = true;
        }
    }

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        if (!Controls.Contains(T))
            Controls.Add(T);
    }

    private void T_MouseHover(object sender, EventArgs e)
    {
        //State = HelperMethods.MouseMode.Hovered;
        Invalidate();
    }

    private void T_MouseLeave(object sender, EventArgs e)
    {
        //State = HelperMethods.MouseMode.Normal;
        Invalidate();
    }

    private void T_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == System.Windows.Forms.MouseButtons.Right)
        {
            if (ContextMenuStrip != null)
                ContextMenuStrip.Show(T, new Point(e.X, e.Y));
        }
        //State = HelperMethods.MouseMode.Normal;
        Invalidate();
    }

    private void T_MouseEnter(object sender, EventArgs e)
    {
        //State = HelperMethods.MouseMode.Normal;
        Invalidate();
    }

    private void T_MouseDown(object sender, EventArgs e)
    {
        //State = HelperMethods.MouseMode.Pushed;
        Invalidate();
    }

    protected override void OnFontChanged(System.EventArgs e)
    {
        base.OnFontChanged(e);
        T.Font = Font;
    }

    protected override void OnSizeChanged(System.EventArgs e)
    {
        base.OnSizeChanged(e);
        T.Size = new Size(Width - 10, Height - 10);
    }

    public void AppendText(string text)
    {
        if (T != null)
        {
            T.AppendText(text);
        }
    }

    public void Undo()
    {
        if (T != null)
        {
            if (T.CanUndo)
            {
                T.Undo();
            }
        }
    }

    public void Redo()
    {
        if (T != null)
        {
            if (T.CanRedo)
            {
                T.Redo();
            }
        }
    }

    public int GetLineFromCharIndex(int index)
    {
        if (T != null)
        {
            return T.GetLineFromCharIndex(index);
        }
        else
        {
            return 0;
        }
    }

    public Point GetPositionFromCharIndex(int index)
    {
        if (T == null)
        {
            return new Point(-1, -1);
        }
        else
        {
            return T.GetPositionFromCharIndex(index);
        }

    }

    public int GetCharIndexFromPosition(System.Drawing.Point pt)
    {
        if (T == null)
            return 0;
        return T.GetCharIndexFromPosition(pt);
    }

    public void ClearUndo()
    {
        if (T == null)
            return;
        T.ClearUndo();
    }

    public void Copy()
    {
        if (T == null)
            return;
        T.Copy();
    }

    public void Cut()
    {
        if (T == null)
            return;
        T.Cut();
    }

    public void SelectAll()
    {
        if (T == null)
            return;
        T.SelectAll();
    }

    public void DeselectAll()
    {
        if (T == null)
            return;
        T.DeselectAll();
    }

    public void Paste(System.Windows.Forms.DataFormats.Format clipFormat)
    {
        if (T == null)
            return;
        T.Paste(clipFormat);
    }

    public void Select(int start, int length)
    {
        if (T == null)
            return;
        T.Select(start, length);
    }

    public void LoadFile(string path)
    {
        if (T == null)
            return;
        T.LoadFile(path);
    }

    public void LoadFile(string path, System.Windows.Forms.RichTextBoxStreamType fileType)
    {
        if (T == null)
            return;
        T.LoadFile(path, fileType);
    }

    public void LoadFile(System.IO.Stream data, System.Windows.Forms.RichTextBoxStreamType fileType)
    {
        if (T == null)
            return;
        T.LoadFile(data, fileType);
    }

    public void SaveFile(string path)
    {
        if (T == null)
            return;
        T.SaveFile(path);
    }

    public void SaveFile(string path, System.Windows.Forms.RichTextBoxStreamType fileType)
    {
        if (T == null)
            return;
        T.SaveFile(path, fileType);
    }
    public void SaveFile(System.IO.Stream data, System.Windows.Forms.RichTextBoxStreamType fileType)
    {
        if (T == null)
            return;
        T.SaveFile(data, fileType);
    }

    public bool CanPaste(System.Windows.Forms.DataFormats.Format clipFormat)
    {
        return T.CanPaste(clipFormat);
    }

    public int Find(char[] characterSet)
    {
        if (T == null)
            return 0;
        return T.Find(characterSet);
    }

    public int Find(char[] characterSet, int start)
    {
        if (T == null)
            return 0;
        return T.Find(characterSet, start);
    }

    public int Find(char[] characterSet, int start, int ends)
    {
        if (T == null)
            return 0;
        return T.Find(characterSet, start, ends);
    }

    public int Find(string str)
    {
        if (T == null)
            return 0;
        return T.Find(str);
    }

    public int Find(string str, int start, int ends, System.Windows.Forms.RichTextBoxFinds options)
    {
        if (T == null)
            return 0;
        return T.Find(str, start, ends, options);
    }

    public int Find(string str, System.Windows.Forms.RichTextBoxFinds options)
    {
        if (T == null)
            return 0;
        return T.Find(str, options);
    }

    public int Find(string str, int start, System.Windows.Forms.RichTextBoxFinds options)
    {
        if (T == null)
            return 0;
        return T.Find(str, start, options);
    }

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;

        GraphicsPath GP = new GraphicsPath();
        Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);

        if (RoundRadius > 0)
        {
            G.SmoothingMode = SmoothingMode.AntiAlias;
            GP = H.RoundRec(Rect, RoundRadius);
        }
        else
        {
            GP.AddRectangle(Rect);
        }

        GP.CloseFigure();

        using (PathGradientBrush BG = new PathGradientBrush(GP) { CenterColor = BaseColor, SurroundColors = new Color[] { ShadowColor }, FocusScales = new PointF(0.98f, 0.95f) })
        using (Pen P = new Pen(ForeColor))
        {
            G.FillPath(BG, GP);
            G.DrawPath(P, GP);
        }


        if (RoundRadius >= 20)
        {
            T.Location = new Point(10, 5);
            T.Width = Width - 20;
        }
        else
        {
            T.Location = new Point(7, 5);
            T.Width = Width - 10;
        }

        GP.Dispose();

    }

    #endregion

}

#endregion

#region Track

#region Horizontal Track

[DefaultEvent("Scroll"), DefaultProperty("Value")]
public class DelightHorizontalTrack : Control
{

    #region Declarations

    private static readonly HelperMethods H = new HelperMethods();
    protected bool Variable;
    private Rectangle Track;
    private Rectangle TrackSide;
    protected int _Maximum = 100;
    private int _Minimum;
    private int _Value;
    private int CurrentValue;
    private Color _BaseColor = Colors.SliderBase;
    private Color _TrackColor = Colors.SliderValue;
    private Color _TrackBaseColor = Colors.SliderThumb;
    private Color _BorderColor = Colors.SliderThumbBorder;
    private Color _ValueTextColor = Colors.SliderBase;

    #endregion

    #region Properties

    [Category("Custom Properties"), Description("Gets or sets the upper limit of the range this TrackBar is working with.")]
    public int Maximum
    {
        get { return _Maximum; }
        set
        {
            _Maximum = value;
            RenewCurrentValue();
            MoveTrack();
            Invalidate();
        }
    }


    [Category("Custom Properties"), Description("Gets or sets the lower limit of the range this TrackBar is working with.")]
    public int Minimum
    {
        get { return _Minimum; }
        set
        {
            if (!(value < 0))
            {
                _Minimum = value;
                RenewCurrentValue();
                MoveTrack();
                Invalidate();
            }
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a numeric value that represents the current position of the scroll box on the track bar.")]
    public int Value
    {
        get { return _Value; }
        set
        {
            if (value != _Value)
            {
                _Value = value;
                RenewCurrentValue();
                MoveTrack();
                Invalidate();
                Scroll?.Invoke(this);
            }
        }
    }


    [Category("Custom Properties"), Description("Gets or sets the value text color of the control.")]
    public Color ValueTextColor
    {
        get { return _ValueTextColor; }
        set
        {
            _ValueTextColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the background color of the control.")]
    public Color BaseColor
    {
        get { return _BaseColor; }
        set
        {
            _BaseColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the border color of the control.")]
    public Color BorderColor
    {
        get { return _BorderColor; }
        set
        {
            _BorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the track handler color of the control.")]
    public Color TrackColor
    {
        get { return _TrackColor; }
        set
        {
            _TrackColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the track handler base color of the control.")]
    public Color TrackBaseColor
    {
        get { return _TrackBaseColor; }
        set
        {
            _TrackBaseColor = value;
            Invalidate();
        }
    }

    #endregion

    #region Constructors

    public DelightHorizontalTrack()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        Font = new Font("Arial", 6);
        UpdateStyles();
    }

    #endregion

    #region Events

    public event ScrollEventHandler Scroll;
    public delegate void ScrollEventHandler(object sender);

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left)
        {
            if (Value == 0)
                return;
            Value -= 1;
        }
        else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Up || e.KeyCode == Keys.Right)
        {
            if (Value == Maximum)
                return;
            Value += 1;
        }
        base.OnKeyDown(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left && Height > 0)
        {
            RenewCurrentValue();
            if (Width > 0 && Height > 0)
            {
                try
                {
                    Track = new Rectangle(CurrentValue, 3, 6, 15);
                }
                catch
                {
                }

            }
            Variable = new Rectangle(CurrentValue, 3, 6, 15).Contains(e.Location);
        }
        base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (Variable && e.X > -1 && e.X < Width + 1)
            Value = Minimum + Convert.ToInt32(unchecked((checked(Maximum - Minimum) * (e.X / (double)Width))));
        base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        Variable = false;
        base.OnMouseUp(e);
    }

    protected override void OnResize(EventArgs e)
    {
        if (Width > 0 && Height > 0)
        {
            RenewCurrentValue();
            MoveTrack();
        }
        Invalidate();
        base.OnResize(e);
        Height = 21;
    }

    public void RenewCurrentValue()
    {
        CurrentValue = Convert.ToInt32(checked((double)(Value - Minimum) / (Maximum - Minimum)) * checked(Width - 7));
        Invalidate();
    }

    protected void MoveTrack()
    {
        if (Height > 0 && Width > 0)
            Track = new Rectangle(CurrentValue, 3, 6, 15);
        TrackSide = new Rectangle((int)Math.Round(CurrentValue + 3.5), 6, 7, 7);
    }

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;

        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        Cursor = Cursors.Hand;

        using (SolidBrush BG = new SolidBrush(BaseColor))
        {
            G.FillRectangle(BG, new Rectangle(0, 9, Width, 4));
        }

        using (SolidBrush VC = new SolidBrush(TrackColor))
        using (Pen P = new Pen(TrackColor, 1))
        using (SolidBrush TBG = new SolidBrush(TrackBaseColor))
        {
            if (CurrentValue != 0)
                G.FillRectangle(VC, new Rectangle(0, 9, CurrentValue, 4));
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.FillRectangle(TBG, Track);
            G.DrawRectangle(P, Track);
        }



    }

    #endregion

}

#endregion

#region Vertical TrackBar

[DefaultEvent("Scroll"), DefaultProperty("Value")]
public class DelightVerticalTrackBar : Control
{

    #region Declarations

    private static readonly HelperMethods H = new HelperMethods();
    private bool Variable;
    private Rectangle Track;
    private Rectangle TrackSide;
    private int _Maximum = 100;
    private int _Minimum;
    private int _Value;
    private int CurrentValue;
    private Color _BaseColor = Colors.SliderBase;
    private Color _TrackColor = Colors.SliderValue;
    private Color _TrackBaseColor = Colors.SliderThumb;
    private Color _BorderColor = Colors.SliderThumbBorder;
    private Color _ValueTextColor = Colors.SliderBase;

    #endregion

    #region Properties

    [Category("Custom Properties"), Description("Gets or sets the upper limit of the range this TrackBar is working with.")]
    public int Maximum
    {
        get { return _Maximum; }
        set
        {
            _Maximum = value;
            RenewCurrentValue();
            MoveTrack();
            Invalidate();
        }
    }


    [Category("Custom Properties"), Description("Gets or sets the lower limit of the range this TrackBar is working with.")]
    public int Minimum
    {
        get { return _Minimum; }
        set
        {
            if (!(value < 0))
            {
                _Minimum = value;
                RenewCurrentValue();
                MoveTrack();
                Invalidate();
            }
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a numeric value that represents the current position of the scroll box on the track bar.")]
    public int Value
    {
        get { return _Value; }
        set
        {
            if (value != _Value)
            {
                _Value = value;
                RenewCurrentValue();
                MoveTrack();
                Invalidate();
                Scroll?.Invoke(this);
            }
        }
    }


    [Category("Custom Properties"), Description("Gets or sets the value text color of the control.")]
    public Color ValueTextColor
    {
        get { return _ValueTextColor; }
        set
        {
            _ValueTextColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the background color of the control.")]
    public Color BaseColor
    {
        get { return _BaseColor; }
        set
        {
            _BaseColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the border color of the control.")]
    public Color BorderColor
    {
        get { return _BorderColor; }
        set
        {
            _BorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the track handler color of the control.")]
    public Color TrackColor
    {
        get { return _TrackColor; }
        set
        {
            _TrackColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the track handler base color of the control.")]
    public Color TrackBaseColor
    {
        get { return _TrackBaseColor; }
        set
        {
            _TrackBaseColor = value;
            Invalidate();
        }
    }

    #endregion

    #region Constructors

    public DelightVerticalTrackBar()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
        BackColor = Color.Transparent;
        DoubleBuffered = true;
        CurrentValue = Convert.ToInt32(Value / (double)Maximum * Height);
        Font = new Font("Arial", 6);
        UpdateStyles();
    }

    #endregion

    #region Events

    public event ScrollEventHandler Scroll;
    public delegate void ScrollEventHandler(object sender);

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left)
        {
            if (Value == 0)
                return;
            Value -= 1;
        }
        else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Up || e.KeyCode == Keys.Right)
        {
            if (Value == Maximum)
                return;
            Value += 1;
        }
        base.OnKeyDown(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left && Height > 0)
        {
            RenewCurrentValue();
            MoveTrack();
            Variable = new Rectangle(3, CurrentValue, 15, 6).Contains(e.Location);
        }
        base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (Variable && e.Y > -1 && e.Y < Height + 1)
            Value = Minimum + Convert.ToInt32(unchecked(checked(Maximum - Minimum) * ((double)e.Y / Height)));
        base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        Variable = false;
        base.OnMouseUp(e);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        if (Height > 0 && Width > 0)
        {
            RenewCurrentValue();
            MoveTrack();
        }
        Width = 26;
        Invalidate();
    }

    public void RenewCurrentValue()
    {
        CurrentValue = Convert.ToInt32(checked((double)(Value - Minimum) / (Maximum - Minimum)) * (Height - 7));
    }

    public void MoveTrack()
    {
        if (Height > 0 && Width > 0)
            Track = new Rectangle(3, CurrentValue, 15, 6);
        TrackSide = new Rectangle(7, (int)Math.Round((CurrentValue + 3.5)), 7, 7);
    }

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;

        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        Cursor = Cursors.Hand;

        using (SolidBrush BG = new SolidBrush(BaseColor))
        {
            G.FillRectangle(BG, new Rectangle(9, 0, 4, Height));
        }

        using (SolidBrush VC = new SolidBrush(TrackColor))

        using (Pen P = new Pen(TrackColor, 1))

        using (SolidBrush TGB = new SolidBrush(TrackBaseColor))
        {
            if (CurrentValue != 0)
                G.FillRectangle(VC, new Rectangle(9, 0, 4, CurrentValue));
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.FillRectangle(TGB, Track);
            G.DrawRectangle(P, Track);
        }


    }

    #endregion

}

#endregion

#endregion

#region  Panel 

public class DelightPanel : ContainerControl
{

    #region  Declarations 

    private static readonly HelperMethods H = new HelperMethods();
    private Color _BaseColor = Colors.GroupBase;
    private Color _BorderColor = Colors.GroupBorder;
    private Color _HeaderLineColor = Colors.GroupLine;
    private int _RoundRadius = 0;

    #endregion

    #region  Constructors 

    public DelightPanel()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        UpdateStyles();
    }

    #endregion

    #region  Properties 

    [Category("Custom Properties"), Description("Gets or sets the background color for the control.")]
    public Color BaseColor
    {
        get { return _BaseColor; }
        set
        {
            _BaseColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the border color for the control.")]
    public Color BorderColor
    {
        get { return _BorderColor; }
        set
        {
            _BorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the header line color for the control.")]
    public Color HeaderLineColor
    {
        get { return _HeaderLineColor; }
        set
        {
            _HeaderLineColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether the control can Rounded in corners.")]
    public int RoundRadius
    {
        get { return _RoundRadius; }
        set
        {
            _RoundRadius = value;
            Invalidate();
        }
    }

    #endregion

    #region  Draw Control 

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;
        GraphicsPath GP = new GraphicsPath();
        Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);

        if (RoundRadius > 0)
        {
            G.SmoothingMode = SmoothingMode.AntiAlias;
            GP = H.RoundRec(Rect, RoundRadius);
        }
        else
        {
            GP.AddRectangle(Rect);
        }

        GP.CloseFigure();

        using (SolidBrush B = new SolidBrush(BaseColor))
        using (Pen P = new Pen(BorderColor))
        using (Pen P2 = new Pen(HeaderLineColor, 2))
        {
            G.FillPath(B, GP);
            G.DrawPath(P, GP);
            G.SetClip(GP);
            G.DrawLine(P2, 1, 2, Width - 2, 2);
            G.ResetClip();
        }


    }

    #endregion

}

#endregion

#region Toggle
[DefaultEvent("Toggled"), DefaultProperty("Toggle")]
public class LexineToggle : Control
{

    #region Declarations

    private static readonly HelperMethods H = new HelperMethods();
    private bool _Toggled;
    protected HelperMethods.MouseMode State = HelperMethods.MouseMode.Normal;
    private Color _UnCheckedColor = Colors.UnChecked;
    private Color _CheckedColor = Colors.Checked;
    private Color _CheckedBallColor = Colors.CheckedSymbol;
    private Color _UnCheckedBallColor = Colors.UnCheckedSymbol;
    private Color _BorderColor = Colors.UncheckedBorder;

    #endregion

    #region Properties

    [Category("Custom Properties"), Description("Gets or set a value indicating whether the control is in the checked state.")]
    public bool Toggle
    {
        get { return _Toggled; }
        set
        {
            _Toggled = value;
            Toggled?.Invoke(this);
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the border color while unchecked")]
    public Color BorderColor
    {
        get { return _BorderColor; }
        set
        {
            _BorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the switch control color while unchecked")]
    public Color UnCheckedColor
    {
        get { return _UnCheckedColor; }
        set
        {
            _UnCheckedColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the switch control color while checked")]
    public Color CheckedColor
    {
        get { return _CheckedColor; }
        set
        {
            _CheckedColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the switch control ball color while checked")]
    public Color CheckedBallColor
    {
        get { return _CheckedBallColor; }
        set
        {
            _CheckedBallColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the switch control ball color while unchecked")]
    public Color UnCheckedBallColor
    {
        get { return _UnCheckedBallColor; }
        set
        {
            _UnCheckedBallColor = value;
            Invalidate();
        }
    }

    #endregion

    #region Constructors

    public LexineToggle()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
        DoubleBuffered = true;
        Cursor = Cursors.Hand;
        BackColor = Color.Transparent;
        UpdateStyles();
    }

    #endregion

    #region Events

    public event ToggledEventHandler Toggled;
    public delegate void ToggledEventHandler(object sender);

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Size = new Size(37, 21);
        Invalidate();
    }

    protected override void OnClick(EventArgs e)
    {
        _Toggled = !Toggle;
        Toggled?.Invoke(this);
        base.OnClick(e);
        Invalidate();
    }

    protected override void OnTextChanged(System.EventArgs e)
    {
        Invalidate();
        base.OnTextChanged(e);
    }

    protected override void OnMouseHover(EventArgs e)
    {
        base.OnMouseHover(e);
        State = HelperMethods.MouseMode.Hovered;
        Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        State = HelperMethods.MouseMode.Normal;
        Invalidate();
    }

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;
        G.SmoothingMode = SmoothingMode.AntiAlias;
        Rectangle Rect = new Rectangle(0, 0, 36, 20);
        using (SolidBrush BG = new SolidBrush(Toggle ? CheckedColor : UnCheckedColor))
        using (SolidBrush Cir = new SolidBrush(Toggle ? CheckedBallColor : UnCheckedBallColor))
        {
            H.FillRoundedPath(e.Graphics, BG, Rect, 20);
            if (Toggle)
            {
                G.FillEllipse(Cir, new Rectangle(Width - 19, 2, 16, 16));
            }
            else
            {
                G.FillEllipse(Cir, new Rectangle(2, 2, 16, 16));
            }
            H.DrawRoundedPath(e.Graphics, BorderColor, 1, Rect, 20);
        }

    }

    #endregion

}

#endregion

#region  Switch 

[DefaultEvent("CheckedChanged"), DefaultProperty("Switched")]
public class DelightSwitch : Control
{

    #region  Declarations 

    private static readonly HelperMethods H = new HelperMethods();
    private bool _Switch = false;
    private Color _ForeColor = Color.Gray;
    private Color _StateTextColor = Color.WhiteSmoke;
    private Color _UnCheckedColor = Color.White;
    private Color _CheckedColor = H.GetHTMLColor("FF337AB7");
    private Color _CheckedSquareColor = Color.White;
    private Color _UnCheckedSquareColor = Color.White;
    private Color _BorderColor = Colors.UncheckedBorder;

    #endregion

    #region  Properties 

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether the control is switched.")]
    public bool Switched
    {
        get { return _Switch; }
        set
        {
            _Switch = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the border color while unchecked")]
    public Color BorderColor
    {
        get { return _BorderColor; }
        set
        {
            _BorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the switch control color while unchecked")]
    public Color UnCheckedColor
    {
        get { return _UnCheckedColor; }
        set
        {
            _UnCheckedColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the switch control color while checked")]
    public Color CheckedColor
    {
        get { return _CheckedColor; }
        set
        {
            _CheckedColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the switch control ball color while checked")]
    public Color CheckedSquareColor
    {
        get { return _CheckedSquareColor; }
        set
        {
            _CheckedSquareColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the switch control ball color while unchecked")]
    public Color UnCheckedSquareColor
    {
        get { return _UnCheckedSquareColor; }
        set
        {
            _UnCheckedSquareColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the switch state text color.")]
    public Color StateTextColor
    {
        get { return _StateTextColor; }
        set
        {
            _StateTextColor = value;
            Invalidate();
        }
    }

    #endregion

    #region  Constructors 

    public DelightSwitch()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        Cursor = Cursors.Hand;
        Size = new Size(70, 28);
        Font = new Font("Segoe UI", 8);
    }

    #endregion

    #region  Draw Control 


    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;

        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        Rectangle Rect = new Rectangle(0, 0, 65, 31);
        G.SmoothingMode = SmoothingMode.AntiAlias;
        using (SolidBrush BG = new SolidBrush(Switched ? CheckedColor : UnCheckedColor))
        using (SolidBrush Cir = new SolidBrush(Switched ? CheckedSquareColor : UnCheckedSquareColor))
        using (Font F = new Font("Segoe UI", 10, FontStyle.Bold))
        using (SolidBrush TB = new SolidBrush(Switched ? UnCheckedColor : CheckedColor))
        {
            H.FillRoundedPath(G, BG.Color, Rect, 8);
            H.FillRoundedPath(G, Cir.Color, new Rectangle(Width - 31, 0, 30, 31), 8, false, true, false, true);
            G.DrawString(Switched ? "ON" : "OFF", F, TB, Switched ? new RectangleF(1, 1, Width - 31, 31) : new RectangleF(Width - 32, 1, 30, 31), H.SetPosition());
            H.DrawRoundedPath(G, BorderColor, 1, Rect, 8);
        }
        

    }

    #endregion

    #region  Events

    public event CheckedChangedEventHandler CheckedChanged;
    public delegate void CheckedChangedEventHandler(object sender);

     protected override void OnClick(EventArgs e)
    {
        _Switch = !_Switch;
        CheckedChanged?.Invoke(this);
        Invalidate();
        base.OnClick(e);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Size = new Size(66, 32);
    }

    #endregion

}

#endregion

#region  Numerical UPDown 

public class DelightNumericUpDown : Control
{

    #region  Variables 

  private static readonly HelperMethods H = new HelperMethods();
    private int X = 0;
    private int Y = 0;
    private int _Value = 0;
    private int _Maximum = 100;
    private int _Minimum = 0;
    private Color _BaseColor = Color.White;
    private Color _ControllingAreasColor = Colors.InputControlArea;
    private Color _ControllingLinesColor = Colors.InputBorder;
    private Color _ControllersTextColor = Colors.InputText;
    private Color _ValueTextColor = Colors.InputText2;
    private Color _BorderColor = Colors.InputBorder;
    private iStyle _Style;

    #endregion

    #region  Properties 

    [Category("Custom Properties"), Description("Gets or sets the current number of the NumericUpDown.")]
    public int Value
    {
        get { return _Value; }
        set
        {
            if (value <= Maximum & value >= Minimum)
                _Value = value;
            if (value > Maximum)
                _Value = Maximum;
            Invalidate();
        }
    }


    [Category("Custom Properties"), Description("Gets or sets the style number of the NumericUpDown.")]
    public iStyle Style
    {
        get { return _Style; }
        set
        {
            _Style = value;
            Invalidate();
        }
    }


    [Category("Custom Properties"), Description("Gets or sets the maximum number of the NumericUpDown.")]
    public int Maximum
    {
        get { return _Maximum; }
        set
        {
            if (value > Minimum)
                _Maximum = value;
            if (value > _Maximum)
                value = _Maximum;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the minimum number of the NumericUpDown.")]
    public int Minimum
    {
        get { return _Minimum; }
        set
        {
            if (value < Maximum)
                _Minimum = value;
            if (value < _Minimum)
                value = _Minimum;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the background color of the control.")]
    public Color BaseColor
    {
        get { return _BaseColor; }
        set
        {
            _BaseColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the plus and minus area background color of the control.")]
    public Color ControllingAreasColor
    {
        get { return _ControllingAreasColor; }
        set
        {
            _ControllingAreasColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the lines of plus and minus area color of the control.")]
    public Color ControllingLinesColor
    {
        get { return _ControllingLinesColor; }
        set
        {
            _ControllingLinesColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the plus and minus symbols color of the control.")]
    public Color ControllersTextColor
    {
        get { return _ControllersTextColor; }
        set
        {
            _ControllersTextColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the value text color of the control.")]
    public Color ValueTextColor
    {
        get { return _ValueTextColor; }
        set
        {
            _ValueTextColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the border color of the control.")]
    public Color BorderColor
    {
        get { return _BorderColor; }
        set
        {
            _BorderColor = value;
            Invalidate();
        }
    }

    #endregion

    #region  Constructors 

    public DelightNumericUpDown()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        UpdateStyles();
        BackColor = Color.Transparent;
        Font = new Font("Segoe UI", 10);
        _Style = iStyle.Arrows;
    }

    #endregion

    #region  Draw Control 


    protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
    {
        Graphics G = e.Graphics;

        G.SmoothingMode = SmoothingMode.AntiAlias;
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);

        using (SolidBrush B = new SolidBrush(BaseColor))
        using (SolidBrush CR = new SolidBrush(ControllingAreasColor))
        using (Pen P = new Pen(ControllingLinesColor))
        {
            G.FillRectangle(B, Rect);
            G.FillPath(CR, H.RoundRec(new Rectangle(Width - 25, 0, 24, Height - 1), 2));
            G.DrawLine(P, new Point(Width - 25, 1), new Point(Width - 25, Height - 2));
            G.DrawLine(P, new Point(Width - 25, 13), new Point(Width - 1, 13));
        }
            
        

        switch (_Style)
        {
            case iStyle.Arrows:
                using (GraphicsPath AboveWardTriangle = new GraphicsPath())
                using (SolidBrush B = new SolidBrush(Value != Maximum ? ValueTextColor : ControllingLinesColor))
                {
                    AboveWardTriangle.AddLine(Width - 17, 8, Width - 2, 8);
                    AboveWardTriangle.AddLine(Width - 9, 8, Width - 13, 4);
                    AboveWardTriangle.CloseFigure();
                    G.FillPath(B, AboveWardTriangle);
                }

                using (GraphicsPath DownWardTriangle = new GraphicsPath())
                using (SolidBrush B = new SolidBrush(Value > Minimum ? ValueTextColor : ControllingLinesColor))
                {
                    DownWardTriangle.AddLine(Width - 17, 17, Width - 2, 17);
                    DownWardTriangle.AddLine(Width - 9, 17, Width - 13, 21);
                    DownWardTriangle.CloseFigure();
                    G.FillPath(B, DownWardTriangle);
                }


                break;
            case iStyle.PlusMinus:
                using (SolidBrush B = new SolidBrush(Value != Maximum ? ValueTextColor : ControllingLinesColor))
                using (SolidBrush B2 = new SolidBrush(Value > Minimum ? ValueTextColor : ControllingLinesColor))
                using (Font F = new Font(Font.Name, Font.Size - 1))
                {
                    G.DrawString("+", F, B, Width - 19, -2);
                    G.DrawString("-", Font, B2, Width - 18, 10);
                }
                break;
        }

        using (SolidBrush B = new SolidBrush(ValueTextColor))
        {
            G.DrawString(Value.ToString(), Font, B, new Rectangle(0, 0, Width - 18, Height - 1), H.SetPosition());
        }

        using (Pen P = new Pen(BorderColor))
        {
            G.DrawRectangle(P, Rect);
        }

    }

    #endregion

    #region  Events
    protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
    {
        base.OnMouseMove(e);
        X = e.Location.X;
        Y = e.Location.Y;
        Invalidate();
        if (e.X < Width - 25)
            Cursor = Cursors.IBeam;
        else
            Cursor = Cursors.Hand;
    }

    protected override void OnResize(System.EventArgs e)
    {
        base.OnResize(e);
        Height = 26;
    }

    protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
    {
        base.OnMouseClick(e);
        if (X > Width - 17 && X < Width - 3)
        {
            if (Y < 13)
            {
                if ((Value + 1) <= Maximum)
                    Value += 1;
            }
            else
            {
                if ((Value - 1) >= Minimum)
                    Value -= 1;
            }
        }
        Invalidate();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.Down | keyData == Keys.Next)
        {
            if ((Value - 1) >= Minimum)
                Value -= 1;
            Invalidate();
            return true;
        }
        else if (keyData == Keys.Up | keyData == Keys.Back)
        {
            if ((Value + 1) <= Maximum)
                Value += 1;
            Invalidate();
            return true;
        }
        else
        {
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    #endregion

    #region  Enumerators 

    public enum iStyle
    {
        Arrows,
        PlusMinus
    }

    #endregion

}


#endregion

#region  ColorBox 

[DefaultProperty("Text")]
public class DelightColorBox : Control
{

    #region  Declarations 

    private static readonly HelperMethods H = new HelperMethods();
    private Rectangle ControlRectangle;
    private Point _LocatedPosition = new Point(-1, -1);
    private Color _ChosenColor = H.GetHTMLColor("FF2C97DE");
    private string _Text = ColorTranslator.ToHtml(H.GetHTMLColor("FF2C97DE"));
    private Rectangle TextRectangle;
    private Color _BaseColor = Colors.InputBase;
    private Color _ControlAreaColor = Colors.InputControlArea;
    private Color _BorderColor = Colors.InputBorder;
    private Color _TextColor = Colors.InputText;

    private HorizontalAlignment _TextAlignment = HorizontalAlignment.Left;
    #endregion

    #region  Constructors 

    public DelightColorBox()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        UpdateStyles();
        BackColor = Color.Transparent;
        Font = new Font("Segoe UI", 10);
    }

    #endregion

    #region  Properties 

    [Category("Custom Properties"), Description("Gets or sets the text alignment of the control.")]
    public HorizontalAlignment TextAlignment
    {
        get { return _TextAlignment; }
        set
        {
            _TextAlignment = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the text of the hex color that chosen by the user.")]
    public new string Text
    {
        get { return _Text; }
        set
        {
            _Text = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the color that chosen by the user.")]
    public Color ChosenColor
    {
        get { return _ChosenColor; }
        set
        {
            _ChosenColor = value;
            Text = ColorTranslator.ToHtml(value);
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the control color.")]
    public Color BaseColor
    {
        get { return _BaseColor; }
        set
        {
            _BaseColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the Border color of the control.")]
    public Color BorderColor
    {
        get { return _BorderColor; }
        set
        {
            _BorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the ControlAreaColor color.")]
    public Color ControlAreaColor
    {
        get { return _ControlAreaColor; }
        set
        {
            _ControlAreaColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the control text color.")]
    public Color TextColor
    {
        get { return _TextColor; }
        set
        {
            _TextColor = value;
            Invalidate();
        }
    }

    #endregion

    #region  Draw Control 

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;
        Rectangle R = new Rectangle(0, 0, Width - 1, Height - 1);
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        ControlRectangle = new Rectangle(Width - 30, 7, 20, Height - 14);
        TextRectangle = new Rectangle(0, 1, Width - 30, 29);
        StringFormat SF = new StringFormat();

        using (SolidBrush B = new SolidBrush(BaseColor))
        using (Pen P = new Pen(BorderColor))
        using (SolidBrush CR = new SolidBrush(ControlAreaColor))
        using (SolidBrush CCS = new SolidBrush(ChosenColor))
        using (SolidBrush TC = new SolidBrush(TextColor))
        {
            switch (TextAlignment)
            {
                case HorizontalAlignment.Left:
                    SF = new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Center
                    };
                    break;
                case HorizontalAlignment.Center:
                    SF = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    break;
                case HorizontalAlignment.Right:
                    SF = new StringFormat
                    {
                        Alignment = StringAlignment.Far,
                        LineAlignment = StringAlignment.Center
                    };
                    break;
            }
            G.FillRectangle(B, R);
            G.FillRectangle(CR, new Rectangle(Width - 40, 0, 40, Height - 1));
            G.FillRectangle(CCS, ControlRectangle);
            G.DrawLine(P, Width - 40, 1, Width - 40, Height - 1);
            G.DrawString(Text, Font, TC, new RectangleF(1, 0, Width - 40, Height - 1), SF);
            G.DrawRectangle(P, R);
        }
     
        SF.Dispose();
    }

    #endregion

    #region  Events 

    protected override void OnMouseMove(MouseEventArgs e)
    {
        _LocatedPosition = e.Location;
        if (ControlRectangle.Contains(_LocatedPosition) || TextRectangle.Contains(_LocatedPosition))
        {
            Cursor = Cursors.Hand;
        }
        else
        {
            Cursor = Cursors.Default;
        }
        Invalidate();
        base.OnMouseMove(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        _LocatedPosition = new Point(-1, -1);
        Cursor = Cursors.Default;
        Invalidate();
        base.OnMouseLeave(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (ControlRectangle.Contains(_LocatedPosition))
        {
            if (e.Button == MouseButtons.Left)
            {
                ColorDialog CD = new ColorDialog
                {
                    AnyColor = true,
                    AllowFullOpen = true,
                    FullOpen = true
                };
                if (CD.ShowDialog() == DialogResult.OK)
                {
                    ChosenColor = CD.Color;
                    Text = ColorTranslator.ToHtml(CD.Color);
                }
            }
        }
        else if (TextRectangle.Contains(_LocatedPosition))
        {
            Clipboard.SetText(Text);
            MessageBox.Show("The color copied to clipboard.", "DelightColorBox");
        }
        base.OnMouseDown(e);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Height = 30;
    }

    #endregion

}

#endregion

#region DatePicker

[DefaultEvent("ValueChanged"), DefaultProperty("Value")]
public class DelightDatePicker : Control
{

    #region Declarations

    private static readonly HelperMethods H = new HelperMethods();
    private HelperMethods.MouseMode State;
    private int _RoundRadius = 5;
    private bool _IsEnabled = true;
    private Color _NormalColor = Colors.DateBase;
    private Color _NormalBorderColor = Colors.DateBorder;
    private Color _NormalTextColor = Colors.DateText;
    private Color _HoverColor = Colors.DateHover;
    private Color _HoverBorderColor = Colors.DateHoverBorder;
    private Color _HoverTextColor = Colors.DateText;
    private Color _PushedColor = Colors.DatePushed;
    private Color _PushedBorderColor = Colors.DatePushedBorder;
    private Color _PushedTextColor = Colors.DateText;
    public event ValueChangedEventHandler ValueChanged;
    public delegate void ValueChangedEventHandler(object sender, EventArgs e);
    private static DateTimePicker DTP = new DateTimePicker();
    private DateTimePickerFormat _Format = DTP.Format;
    private DateTime _value = DTP.Value;
    private string _Text = DTP.Value.ToLongDateString();
    private string _CustomFormat = string.Empty;
    private DateTime _MinTime = DTP.MinDate;
    private DateTime _MaxTime = DTP.MaxDate;

    #endregion

    #region Constructors

    public DelightDatePicker()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        Font = new Font("Segoe UI", 14, FontStyle.Regular);
        DTP.Location = new Point(55, Height - 1);
        DTP.Size = Size;
        DTP.ValueChanged += DTP_ValueChanged;
        Controls.Add(DTP);
        UpdateStyles();
    }

    #endregion

    #region Properties

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether the control can Rounded in corners.")]
    public int RoundRadius
    {
        get { return _RoundRadius; }
        set
        {
            _RoundRadius = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets a value indicating whether the control can respond to user interaction.")]
    public bool IsEnabled
    {
        get { return _IsEnabled; }
        set
        {
            Enabled = value;
            _IsEnabled = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the control color in normal mouse state.")]
    public Color NormalColor
    {
        get { return _NormalColor; }
        set
        {
            _NormalColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the control border color in normal mouse state.")]
    public Color NormalBorderColor
    {
        get { return _NormalBorderColor; }
        set
        {
            _NormalBorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the control Text color in normal mouse state.")]
    public Color NormalTextColor
    {
        get { return _NormalTextColor; }
        set
        {
            _NormalTextColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the control color in hover mouse state.")]
    public Color HoverColor
    {
        get { return _HoverColor; }
        set
        {
            _HoverColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the control border color in hover mouse state.")]
    public Color HoverBorderColor
    {
        get { return _HoverBorderColor; }
        set
        {
            _HoverBorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the control Text color in hover mouse state.")]
    public Color HoverTextColor
    {
        get { return _HoverTextColor; }
        set
        {
            _HoverTextColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the control color in mouse down state.")]
    public Color PushedColor
    {
        get { return _PushedColor; }
        set
        {
            _PushedColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the control border color in mouse down state.")]
    public Color PushedBorderColor
    {
        get { return _PushedBorderColor; }
        set
        {
            _PushedBorderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties"), Description("Gets or sets the the control Text color in mouse down state.")]
    public Color PushedTextColor
    {
        get { return _PushedTextColor; }
        set
        {
            _PushedTextColor = value;
            Invalidate();
        }
    }

    [Category("Custom Properties")]
    [Description("Gets or sets the maximum date and time that can be selected in the control.")]
    public DateTime MaxDate
    {
        get { return _MaxTime; }
        set
        {
            _MaxTime = value;
            DTP.MaxDate = value;
            Invalidate();
        }
    }

    [Category("Custom Properties")]
    [Description("Gets or sets the minimum date and time that can be selected in the control.")]
    public DateTime MinDate
    {
        get { return _MinTime; }
        set
        {
            _MinTime = value;
            DTP.MinDate = value;
            Invalidate();
        }
    }

    [Category("Custom Properties")]
    [Description("Gets or sets the date/time value assigned to the control.")]
    public DateTime Value
    {
        get { return _value; }
        set
        {
            if (value > MaxDate || value < MinDate)
                return;
            _value = value;
            DTP.Value = _value;
            Text = DTP.Value.ToString();
            Invalidate();
        }
    }

    [Category("Custom Properties")]
    [Description("Gets or sets the custom date/time format string.")]
    public string CustomFormat
    {
        get { return _CustomFormat; }
        set
        {
            _CustomFormat = value;
            DTP.CustomFormat = value;
            Invalidate();
        }
    }

    [Category("Custom Properties")]
    [Description("Gets or sets the format of the date and time displayed in the control.")]
    public DateTimePickerFormat Format
    {
        get { return _Format; }
        set
        {
            _Format = value;
            DTP.Format = value;
            if (value == DateTimePickerFormat.Long)
            {
                Text = DTP.Value.ToLongDateString();
            }
            else if (value == DateTimePickerFormat.Short)
            {
                Text = DTP.Value.ToShortDateString();
            }
            else if (value == DateTimePickerFormat.Time)
            {
                Text = DTP.Value.ToLongTimeString();
            }
            else if (value == DateTimePickerFormat.Custom)
            {
                _CustomFormat = MaxDate.ToString();
                Text = DTP.Value.ToString();
            }
            Invalidate();
        }
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public override string Text
    {
        get { return _Text; }
        set
        {
            _Text = value;
            Invalidate();
        }
    }


    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics G = e.Graphics;
        GraphicsPath GP = new GraphicsPath();
        Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);

        if (RoundRadius > 0)
        {
            G.SmoothingMode = SmoothingMode.AntiAlias;
            GP = H.RoundRec(Rect, RoundRadius);
        }
        else
        {
            GP.AddRectangle(Rect);
        }

        GP.CloseFigure();

        G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

        switch (State)
        {

            case HelperMethods.MouseMode.Normal:

                using (SolidBrush BG = new SolidBrush(NormalColor))
                using (Pen P = new Pen(NormalBorderColor))
                {
                    G.FillPath(BG, GP);
                    G.DrawPath(P, GP);
                }

                using (SolidBrush B = new SolidBrush(NormalTextColor))
                {
                    G.DrawString(Text, Font, B, Rect, H.SetPosition());
                }

                G.SmoothingMode = SmoothingMode.AntiAlias;

                H.DrawTriangle(G, NormalTextColor, Convert.ToInt32(1.5), new Point(Width - 20, 14), new Point(Width - 16, 18), new Point(Width - 16, 18), new Point(Width - 12, 14), new Point(Width - 16, 19), new Point(Width - 16, 18));

                break;
            case HelperMethods.MouseMode.Hovered:

                Cursor = Cursors.Hand;

                using (SolidBrush BG = new SolidBrush(HoverColor))
                using (Pen P = new Pen(HoverBorderColor))
                {
                    G.FillPath(BG, GP);
                    G.DrawPath(P, GP);
                }


                using (SolidBrush B = new SolidBrush(HoverTextColor))
                {
                    G.DrawString(Text, Font, B, Rect, H.SetPosition());
                }

                G.SmoothingMode = SmoothingMode.AntiAlias;

                H.DrawTriangle(G, HoverTextColor, Convert.ToInt32(1.5), new Point(Width - 20, 14), new Point(Width - 16, 18), new Point(Width - 16, 18), new Point(Width - 12, 14), new Point(Width - 16, 19), new Point(Width - 16, 18));

                break;
            case HelperMethods.MouseMode.Pushed:

                using (SolidBrush BG = new SolidBrush(PushedColor))
                using (Pen P = new Pen(PushedBorderColor))
                {
                    G.FillPath(BG, GP);
                    G.DrawPath(P, GP);
                }


                using (SolidBrush B = new SolidBrush(PushedTextColor))
                {
                    G.DrawString(Text, Font, B, Rect, H.SetPosition());
                }

                G.SmoothingMode = SmoothingMode.AntiAlias;

                H.DrawTriangle(G, PushedTextColor, Convert.ToInt32(1.5), new Point(Width - 20, 14), new Point(Width - 16, 18), new Point(Width - 16, 18), new Point(Width - 12, 14), new Point(Width - 16, 19), new Point(Width - 16, 18));

                break;
        }

    }

    #endregion

    #region Events

    private void DTP_ValueChanged(object sender, EventArgs e)
    {
        Text = DTP.Value.ToLongDateString();
        _value = DTP.Value;
        ValueChanged?.Invoke(this, e);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Size = new Size(330, 32);
    }

    protected override void OnClick(EventArgs e)
    {
        DTP.Select();
        SendKeys.Send("%{DOWN}");
        base.OnClick(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        State = HelperMethods.MouseMode.Hovered;
        Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        State = HelperMethods.MouseMode.Pushed;
        Invalidate();
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        State = HelperMethods.MouseMode.Hovered;
        Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseEnter(e);
        State = HelperMethods.MouseMode.Normal;
        Invalidate();
    }

    #endregion

}


#endregion

#region ContextMenuStrip

public class DelightContextMenuStrip : ContextMenuStrip
{

    #region Variables

    private ToolStripItemClickedEventArgs ClickedEventArgs;
    private static readonly HelperMethods H = new HelperMethods();

    #endregion

    #region Constructors

    public DelightContextMenuStrip()
    {
        Renderer = new DelightToolStripRender();
        BackColor = Colors.ContextBackground;
    }

    #endregion

    #region Events

    public event ClickedEventHandler Clicked;
    public delegate void ClickedEventHandler(object sender);

    protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
    {
        if ((e.ClickedItem != null) && !(e.ClickedItem is ToolStripSeparator))
        {
            if (ReferenceEquals(e, ClickedEventArgs))
                OnItemClicked(e);
            else
            {
                ClickedEventArgs = e; Clicked?.Invoke(this);
            }
        }
    }

    protected override void OnMouseHover(EventArgs e)
    {
        base.OnMouseHover(e);
        Cursor = Cursors.Hand;
        Invalidate();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        Cursor = Cursors.Hand;
        Invalidate();
    }

    #endregion

    #region ToolStripMenuItem

    public sealed class DelightToolStripMenuItem : ToolStripMenuItem
    {

        #region Constructors

        public DelightToolStripMenuItem()
        {
            AutoSize = false;
            Size = new Size(160, 30);
        }

        #endregion

        #region Adding DropDowns

        protected override ToolStripDropDown CreateDefaultDropDown()
        {
            if (DesignMode)
            { return base.CreateDefaultDropDown(); }
            DelightContextMenuStrip DP = new DelightContextMenuStrip();
            DP.Items.AddRange(base.CreateDefaultDropDown().Items);
            return DP;
        }

        #endregion

    }

    #endregion

    #region ToolStripRender

    public sealed class DelightToolStripRender : ToolStripProfessionalRenderer
    {

        #region Drawing Text

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            Rectangle textRect = new Rectangle(25, e.Item.ContentRectangle.Y, e.Item.ContentRectangle.Width - (24 + 16), e.Item.ContentRectangle.Height - 4);

            using (SolidBrush B = new SolidBrush(e.Item.Selected && e.Item.Enabled ? Colors.EnabledItemText : Colors.DisabledItemText))
            using (Font F = new Font("Segoe UI", 10))
            {
                e.Graphics.DrawString(e.Text, F, B, textRect);
            }


        }

        #endregion

        #region Drawing Backgrounds

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.High;
            e.Graphics.Clear(Colors.ContextBackground);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.High;
            e.Graphics.Clear(Colors.ContextBackground);
            Rectangle R = new Rectangle(0, e.Item.ContentRectangle.Y - 2, e.Item.ContentRectangle.Width + 4, e.Item.ContentRectangle.Height + 3);

            using (SolidBrush B = new SolidBrush(e.Item.Selected && e.Item.Enabled ? Colors.ItemSelected : Colors.ItemUnSelected))
            {
                e.Graphics.FillRectangle(B, R);
            }

        }

        #endregion

        #region Set Image Margin

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            //MyBase.OnRenderImageMargin(e) 
            //I Make above line comment which makes users to be able to add images to ToolStrips
        }

        #endregion

        #region Drawing Seperators & Borders

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen P = new Pen(Colors.ItemBorder))
            {
                e.Graphics.DrawLine(P, new Point(e.Item.Bounds.Left, e.Item.Bounds.Height / 2),
                    new Point(e.Item.Bounds.Right - 5, e.Item.Bounds.Height / 2));
            }

        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.High;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            H.DrawRoundedPath(e.Graphics, Colors.ItemBorder, 1, new Rectangle(e.AffectedBounds.X, e.AffectedBounds.Y, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1), 4);
        }

        #endregion

        #region Drawing DropDown Arrows

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            int ArrowX, ArrowY;
            ArrowX = e.ArrowRectangle.X + e.ArrowRectangle.Width / 2;
            ArrowY = e.ArrowRectangle.Y + e.ArrowRectangle.Height / 2;
            Point[] ArrowPoints = new Point[] {
                new Point(ArrowX - 5, ArrowY - 5),
                new Point(ArrowX, ArrowY),
                new Point(ArrowX - 5, ArrowY + 5)
            };

            using (SolidBrush ArrowBrush = new SolidBrush(e.Item.Enabled ? Colors.EnabledArrows : Colors.DisabledArrows))
            {
                e.Graphics.FillPolygon(ArrowBrush, ArrowPoints);
            }

        }

        #endregion

    }

    #endregion

}

#endregion

#region ToolTip
public class DelightToolTip : ToolTip, INotifyPropertyChanged
{

    #region Declarations

    private static readonly HelperMethods H = new HelperMethods();
    private Color _BackColor = Color.White;
    private Color _BorderColor = Color.Gray;
    private string errIMG = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAABh0RVh0U29mdHdhcmUAcGFpbnQubmV0IDQuMC45bDN+TgAAANBJREFUOE+tkrEVwyAMRN2m8whsklW8SbxB2CTexGyQUQjfkhLLgBunuPfE3UlIgiHnXON+iwVrQVYQP1teT0jiW5OWgllBDIfmCu2Tk5pIGPcm1UfVpCPlTeRmhOmbIDxcCR03KR85QwQlZmcUc11AeOskcLDbW233CjAOWuTA7EtlEmO7gGgsdjVT3b6YzgpsY/ylQH+EM8gIiaC/xB4OS7z4jEJc+Eg/wb7yo6D3ldHwJOOPJusEvApIAMTGbzcbfAEgO6GQdQSI4YL35+ED+tuFv78OZr4AAAAASUVORK5CYII=";
    private string excIMG = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNvyMY98AAADNSURBVDhPnZJdDcMwDIQNYRAGYRAGYRACZSTyXiYrg4VBC6EMMsc/SZy407SHT7LvXN0pKuScz4mwIi/XE1yRiBCQLAT3BnFFIsKOHMLm3iCuSImc/BROW0wCwenlo4tQZrfFJOChpi+dtog2tTAL0dKvSELeMrstzIIHNp1nHGl2W9SB6NN5Lw2SzDfxTIs6oKHpa6eVH2ncTQs22NT0e9VGije0UGNOZ50f0WqmhYqa/hiO2xs0TcOoRS/s5vAbLTD0yz9sWtMzfwDSB4MxskcE6XapAAAAAElFTkSuQmCC";
    private string infIMG = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAABh0RVh0U29mdHdhcmUAcGFpbnQubmV0IDQuMC45bDN+TgAAANNJREFUOE+lktENwjAMRDtCR8hoIAahG5BN6Bj8tRt0lOBX2ZC4sUDw8aTk7uw4bYZSyoHz45KFRSgK61sv22wkROGmRbMwKazR8JpGdfGqIQrGOqT+qN4+kelmcjLGyYwIMprN7BGSClMnjC7Lg26TpPr03thRA66Dl9lw99mHPkGNsNgp4fjgPaAGL2ygoa8ahFcgAF4Haqhl8fdH7P5G1V447/0bVfj9IVWGPeWrED1lPDKr6T5kk8BdoABYm76fbDQNQAJ8ExrZRMAaLbX5MjwByiSlG0FA+zcAAAAASUVORK5CYII=";

    #endregion

    #region Properties

    public new Color BackColor
    {
        get { return _BackColor; }
        set
        {
            _BackColor = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BackColor"));
        }
    }

    public Color BorderColor
    {
        get { return _BorderColor; }
        set
        {
            _BorderColor = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BorderColor"));
        }
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new bool IsBalloon { get; set; }

    #endregion

    #region Events

    public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);

    #endregion

    #region Draw Control

    private void OnDraw(object sender, DrawToolTipEventArgs e)
    {
        Graphics G = e.Graphics;
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        G.InterpolationMode = InterpolationMode.HighQualityBicubic;

        using (SolidBrush BG = new SolidBrush(BackColor))
        {
            G.FillRectangle(BG, e.Bounds);
        }

        string Icon = "";
        SolidBrush B = H.SolidBrushHTMlColor("FF61C863");
        Rectangle IconRect = new Rectangle(5, 5, 16, 16);
        Point TitleTipRect = new Point(IconRect.X, e.Bounds.Y);
        Font F = new Font("Segoe UI", 9, FontStyle.Bold);

        switch (ToolTipIcon)
        {
            case ToolTipIcon.None:
                Icon = "";
                B = H.SolidBrushHTMlColor("0056AD");
                TitleTipRect = new Point(5, 1);
                break;
            case ToolTipIcon.Warning:
                Icon = excIMG;
                B = H.SolidBrushHTMlColor("FFFF9500");
                TitleTipRect = new Point(20, e.Bounds.Y + 3);
                break;
            case ToolTipIcon.Info:
                Icon = infIMG;
                B = H.SolidBrushHTMlColor("FF61C863");
                TitleTipRect = new Point(20, e.Bounds.Y + 3);
                break;
            case ToolTipIcon.Error:
                Icon = errIMG;
                B = H.SolidBrushHTMlColor("FFFF3F09");
                TitleTipRect = new Point(20, e.Bounds.Y + 3);
                break;
        }

        if (!string.IsNullOrEmpty(Icon))
        {
            H.DrawImageFromBase64(G, Icon, IconRect);
        }
        using (Font FF = new Font("Segoe UI", 9))
        {
            if (!string.IsNullOrEmpty(ToolTipTitle))
            {
                G.DrawString(ToolTipTitle, F, B, TitleTipRect);
                G.DrawString(e.ToolTipText, FF, H.SolidBrushHTMlColor("B2B2B2"), new PointF(TitleTipRect.X + 1, e.Graphics.MeasureString(ToolTipTitle, F).Height + 1));
            }
            else
            {
                G.DrawString(e.ToolTipText, FF, H.SolidBrushHTMlColor("B2B2B2"), TitleTipRect);
            }
        }
        using (Pen Stroke = new Pen(BorderColor))
        {
            G.DrawRectangle(Stroke, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1));
        }
        F.Dispose();
        B.Dispose();

    }

    #endregion

    #region Constructors

    public DelightToolTip()
    {
        Draw += OnDraw;
        OwnerDraw = true;
    }
    #endregion

}

#endregion

#region  Colors 

sealed class Colors
{

    #region  TabControl 

    public static Color TabColor
    {
        get { return Color.FromArgb(255, 39, 38, 52); }
    }

    public static Color TabColor2
    {
        get { return Color.FromArgb(255, 31, 31, 42); }
    }

    public static Color TabHeader
    {
        get { return Color.FromArgb(255, 144, 143, 150); }
    }

    public static Color TabSelected
    {
        get { return Color.FromArgb(255, 195, 193, 232); }
    }

    public static Color TabSelected2
    {
        get { return Color.FromArgb(255, 233, 30, 99); }
    }

    public static Color TabUnSelected
    {
        get { return Color.FromArgb(255, 127, 127, 146); }
    }

    public static Color TabUnSelected2
    {
        get { return Color.FromArgb(255, 169, 169, 183); }
    }

    public static Color TabClear
    {
        get { return Color.FromArgb(255, 244, 245, 247); }
    }
    public static Color White
    {
        get { return Color.White; }
    }

    #endregion

    #region  Button 

    public static Color ButtonBase
    {
        get { return Color.FromArgb(255, 254, 254, 255); }
    }

    public static Color ButtonBorder
    {
        get { return Color.FromArgb(255, 239, 241, 246); }
    }

    public static Color ButtonText
    {
        get { return Color.FromArgb(255, 87, 91, 97); }
    }

    public static Color ButtonBaseHover
    {
        get { return Color.FromArgb(255, 243, 243, 249); }
    }

    public static Color ButtonHoverBorder
    {
        get { return Color.FromArgb(255, 243, 243, 249); }
    }

    public static Color ButtonHoverText
    {
        get { return Color.FromArgb(255, 43, 47, 51); }
    }

    public static Color ButtonBasePushed
    {
        get { return Color.FromArgb(255, 243, 243, 249); }
    }

    public static Color ButtonPushedBorder
    {
        get { return Color.FromArgb(255, 220, 216, 231); }
    }

    public static Color ButtonPushedText
    {
        get { return Color.FromArgb(255, 43, 47, 51); }
    }

    #endregion

    #region  Seperator & Labels 

    public static Color Label
    {
        get { return Color.FromArgb(255, 62, 72, 85); }
    }

    public static Color LinkNormal
    {
        get { return Color.FromArgb(255, 51, 185, 246); }
    }

    public static Color LinkActive
    {
        get { return Color.FromArgb(255, 112, 126, 200); }
    }

    public static Color LinkVisited
    {
        get { return Color.FromArgb(255, 0, 188, 212); }
    }

    public static Color Seperator
    {
        get { return Color.FromArgb(255, 247, 247, 247); }
    }

    #endregion

    #region  CheckBase 

    public static Color UncheckedBase
    {
        get { return Color.FromArgb(255, 254, 254, 255); }
    }

    public static Color CheckedBase
    {
        get { return Color.FromArgb(255, 94, 183, 97); }
    }

    public static Color UncheckedBorder
    {
        get { return Color.FromArgb(255, 240, 242, 247); }
    }

    public static Color CheckedBorder
    {
        get { return Color.FromArgb(255, 82, 171, 86); }
    }

    public static Color Un_CheckedText
    {
        get { return Color.FromArgb(255, 87, 91, 97); }
    }

    public static Color Checked
    {
        get { return Color.FromArgb(255, 233, 30, 99); }
    }

    public static Color UnChecked
    {
        get { return Color.White; }
    }

    public static Color UnCheckedSymbol
    {
        get { return Color.White; }
    }
    public static Color CheckedSymbol
    {
        get { return Color.FromArgb(255, 233, 30, 99); }
    }


    #endregion

    #region  InputBase 

    public static Color InputBase
    {
        get { return Color.White; }
    }

    public static Color InputShadow
    {
        get { return Color.WhiteSmoke; }
    }

    public static Color InputControlArea
    {
        get { return Color.FromArgb(255, 254, 254, 255); }
    }

    public static Color InputText
    {
        get { return Color.FromArgb(255, 43, 47, 51); }
    }
    public static Color DateBase
    {
        get { return Color.FromArgb(255, 233, 30, 99); }
    }
    public static Color DateBorder
    {
        get { return Color.FromArgb(255, 221, 21, 89); }
    }

    public static Color DateHover
    {
        get { return Color.FromArgb(255, 246, 43, 112); }
    }
    public static Color DateHoverBorder
    {
        get { return Color.FromArgb(255, 220, 17, 86); }
    }
    public static Color DatePushed
    {
        get { return Color.FromArgb(255, 233, 30, 99); }
    }
    public static Color DatePushedBorder
    {
        get { return Color.FromArgb(255, 220, 17, 86); }
    }
    public static Color DateText
    {
        get { return Color.White; }
    }

    public static Color InputText2
    {
        get { return Color.FromArgb(255, 87, 91, 97); }
    }

    public static Color InputSelected
    {
        get { return Color.FromArgb(255, 39, 38, 52); }
    }

    public static Color InputBorder
    {
        get { return Color.Silver; }
    }


    #endregion

    #region  Alert 

    public static Color AlertSuccessBase
    {
        get { return Color.FromArgb(255, 211, 255, 198); }
    }
    public static Color AlertSuccessTitle
    {
        get { return Color.FromArgb(255, 40, 107, 25); }
    }

    public static Color AlertSuccessContent
    {
        get { return Color.FromArgb(255, 139, 193, 125); }
    }
    public static Color AlertSuccessImageArea
    {
        get { return Color.FromArgb(255, 76, 175, 80); }
    }
    public static Color AlertSuccessImage
    {
        get { return Color.White; }
    }

    public static Color AlertInfoBase
    {
        get { return Color.FromArgb(255, 223, 232, 241); }
    }

    public static Color AlertInfoTitle
    {
        get { return Color.FromArgb(255, 120, 121, 122); }
    }

    public static Color AlertInfoContent
    {
        get { return Color.FromArgb(255, 162, 166, 170); }
    }
    public static Color AlertInfoImageArea
    {
        get { return Color.FromArgb(255, 39, 38, 52); }
    }

    public static Color AlertInfoImage
    {
        get { return Color.FromArgb(255, 204, 204, 204); }
    }

    public static Color AlertNoticeBase
    {
        get { return Color.FromArgb(255, 198, 232, 255); }
    }
    public static Color AlertNoticeTitle
    {
        get { return Color.FromArgb(255, 43, 73, 122); }
    }

    public static Color AlertNoticeContent
    {
        get { return Color.FromArgb(255, 126, 158, 194); }
    }

    public static Color AlertNoticeImageArea
    {
        get { return Color.FromArgb(255, 33, 150, 243); }
    }

    public static Color AlertNoticeImage
    {
        get { return Color.White; }
    }

    public static Color AlertWarningBase
    {
        get { return Color.FromArgb(255, 255, 238, 198); }
    }
    public static Color AlertWarningTitle
    {
        get { return Color.FromArgb(255, 140, 118, 65); }
    }

    public static Color AlertWarningContent
    {
        get { return Color.FromArgb(255, 222, 203, 159); }
    }

    public static Color AlertWarningImageArea
    {
        get { return Color.FromArgb(255, 255, 87, 34); }
    }

    public static Color AlertWarningImage
    {
        get { return Color.White; }
    }

    public static Color AlertDangerBase
    {
        get { return Color.FromArgb(255, 255, 198, 198); }
    }
    public static Color AlertDangerTitle
    {
        get { return Color.FromArgb(255, 114, 33, 33); }
    }

    public static Color AlertDangerContent
    {
        get { return Color.FromArgb(255, 208, 143, 143); }
    }

    public static Color AlertDangerImageArea
    {
        get { return Color.FromArgb(255, 244, 67, 54); }
    }

    public static Color AlertDangerImage
    {
        get { return Color.White; }
    }
    #endregion

    #region  Progress 

    public static Color ProgressBase
    {
        get { return Color.FromArgb(255, 254, 254, 255); }
    }

    public static Color Progress
    {
        get { return Color.FromArgb(255, 39, 38, 52); }
    }

    public static Color Progress2
    {
        get { return Color.FromArgb(255, 31, 31, 42); }
    }

    public static Color ProgressBorder
    {
        get { return Color.FromArgb(255, 239, 241, 246); }
    }

    #endregion

    #region  GroupBox & Panels 

    public static Color GroupBase
    {
        get { return Color.White; }
    }

    public static Color GroupBorder
    {
        get { return Color.FromArgb(255, 247, 247, 247); }
    }

    public static Color GroupText
    {
        get { return Color.FromArgb(255, 62, 72, 85); }
    }

    public static Color GroupLine
    {
        get { return Color.FromArgb(255, 233, 30, 99); }
    }

    #endregion

    #region  Slider 

    public static Color SliderBase
    {
        get { return Color.FromArgb(255, 239, 241, 246); }
    }

    public static Color SliderValue
    {
        get { return Color.FromArgb(255, 39, 38, 52); }
    }

    public static Color SliderThumb
    {
        get { return Color.FromArgb(255, 39, 38, 52); }
    }

    public static Color SliderThumbBorder
    {
        get { return Color.FromArgb(255, 31, 31, 42); }
    }

    #endregion

    #region  Scrollbar 

    public static Color ScrollbarBase
    {
        get { return Color.White; }
    }

    public static Color ScrollbarArrow
    {
        get { return Color.FromArgb(255, 39, 38, 52); }
    }

    public static Color ScrollbarThumb
    {
        get { return Color.FromArgb(255, 39, 38, 52); }
    }

    public static Color ScrollbarBorder
    {
        get { return Color.White; }
    }

    #endregion

    #region Context

    public static Color ContextBackground
    {
        get { return Color.White; }
    }

    public static Color ItemSelected
    {
        get { return Color.FromArgb(255, 233, 30, 99); }
    }

    public static Color ItemUnSelected
    {
        get { return Color.White; }
    }

    public static Color ItemBorder
    {
        get { return Color.Silver; }
    }

    public static Color DisabledArrows
    {
        get { return Color.WhiteSmoke; }
    }

    public static Color EnabledArrows
    {
        get { return Color.Silver; }
    }

    public static Color DisabledItemText
    {
        get { return Color.Silver; }
    }

    public static Color EnabledItemText
    {
        get { return Color.White; }
    }

    #endregion

}

#endregion

