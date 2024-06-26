#region PDFsharp Charting - A .NET charting library based on PDFsharp
//
// Authors:
//   Niklas Schneider (mailto:Niklas.Schneider@PdfSharpCore.com)
//
// Copyright (c) 2005-2009 empira Software GmbH, Cologne (Germany)
//
// http://www.PdfSharpCore.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

using PdfSharpCore.Drawing;

namespace PdfSharpCore.Charting.Renderers
{
    /// <summary>
    /// Provides functions which converts Charting.DOM objects into PdfSharpCore.Drawing objects.
    /// </summary>
    internal class Converter
    {
        /// <summary>
        /// Creates a XFont based on the font. Missing attributes will be taken from the defaultFont
        /// parameter.
        /// </summary>
        internal static XFont ToXFont(Font font, XFont defaultFont)
        {
            XFont xfont = defaultFont;
            if (font != null)
            {
                string fontFamily = font.name;
                if (fontFamily == "")
                    fontFamily = defaultFont.Name;

                XFontStyle fontStyle = defaultFont.Style;
                if (font.bold)
                    fontStyle |= XFontStyle.Bold;
                if (font.italic)
                    fontStyle |= XFontStyle.Italic;

                double size = font.size.Point; //emSize???
                if (size == 0)
                    size = defaultFont.Size;

                xfont = new XFont(fontFamily, size, fontStyle);
            }
            return xfont;
        }

        /// <summary>
        /// Creates a XPen based on the specified line format. If not specified color and width will be taken
        /// from the defaultColor and defaultWidth parameter.
        /// </summary>
        internal static XPen ToXPen(LineFormat lineFormat, XColor defaultColor, double defaultWidth)
        {
            return ToXPen(lineFormat, defaultColor, defaultWidth, XDashStyle.Solid);
        }

        /// <summary>
        /// Creates a XPen based on the specified line format. If not specified color and width will be taken
        /// from the defaultPen parameter.
        /// </summary>
        internal static XPen ToXPen(LineFormat lineFormat, XPen defaultPen)
        {
            return ToXPen(lineFormat, defaultPen.Color, defaultPen.Width, defaultPen.DashStyle);
        }

        /// <summary>
        /// Creates a XPen based on the specified line format. If not specified color, width and dash style
        /// will be taken from the defaultColor, defaultWidth and defaultDashStyle parameters.
        /// </summary>
        internal static XPen ToXPen(LineFormat lineFormat, XColor defaultColor, double defaultWidth, XDashStyle defaultDashStyle)
        {
            XPen pen = null;
            if (lineFormat == null)
            {
                pen = new XPen(defaultColor, defaultWidth);
                pen.DashStyle = defaultDashStyle;
            }
            else
            {
                XColor color = defaultColor;
                if (!lineFormat.Color.IsEmpty)
                    color = lineFormat.Color;

                double width = lineFormat.Width.Point;
                if (!lineFormat.Visible)
                    width = 0;
                if (lineFormat.Visible && width == 0)
                    width = defaultWidth;

                pen = new XPen(color, width);
                pen.DashStyle = lineFormat.dashStyle;
                pen.DashOffset = 10 * width;
            }
            return pen;
        }

        /// <summary>
        /// Creates a XBrush based on the specified fill format. If not specified, color will be taken
        /// from the defaultColor parameter.
        /// </summary>
        internal static XBrush ToXBrush(FillFormat fillFormat, XColor defaultColor)
        {
            if (fillFormat == null || fillFormat.color.IsEmpty)
                return new XSolidBrush(defaultColor);
            return new XSolidBrush(fillFormat.color);
        }

        /// <summary>
        /// Creates a XBrush based on the specified font color. If not specified, color will be taken
        /// from the defaultColor parameter.
        /// </summary>
        internal static XBrush ToXBrush(Font font, XColor defaultColor)
        {
            if (font == null || font.color.IsEmpty)
                return new XSolidBrush(defaultColor);
            return new XSolidBrush(font.color);
        }
    }
}
