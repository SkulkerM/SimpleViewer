﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.6.1055.0.
// 
namespace SimplePDL {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schemas.foobar.com/SimplePDL/1.0/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://schemas.foobar.com/SimplePDL/1.0/", IsNullable=false)]
    public partial class Page {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Canvas", typeof(ctCanvas))]
        [System.Xml.Serialization.XmlElementAttribute("Circle", typeof(ctCircle))]
        [System.Xml.Serialization.XmlElementAttribute("Line", typeof(ctLine))]
        [System.Xml.Serialization.XmlElementAttribute("Rectangle", typeof(ctRectangle))]
        [System.Xml.Serialization.XmlElementAttribute("Text", typeof(ctText))]
        public object[] Items;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Dimensions;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.foobar.com/SimplePDL/1.0/")]
    public partial class ctCanvas {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Canvas", typeof(ctCanvas))]
        [System.Xml.Serialization.XmlElementAttribute("Circle", typeof(ctCircle))]
        [System.Xml.Serialization.XmlElementAttribute("Line", typeof(ctLine))]
        [System.Xml.Serialization.XmlElementAttribute("Rectangle", typeof(ctRectangle))]
        [System.Xml.Serialization.XmlElementAttribute("Text", typeof(ctText))]
        public object[] Items;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Transform;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double Opacity;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OpacitySpecified;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.foobar.com/SimplePDL/1.0/")]
    public partial class ctCircle {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Transform;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double Opacity;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OpacitySpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Center;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double Radius;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Fill;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Stroke;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double StrokeWidth;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool StrokeWidthSpecified;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.foobar.com/SimplePDL/1.0/")]
    public partial class ctText {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Transform;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double Opacity;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OpacitySpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Origin;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Font;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double Size;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Color;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string String;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.foobar.com/SimplePDL/1.0/")]
    public partial class ctLine {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Transform;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double Opacity;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OpacitySpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Points;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Color;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double Width;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.foobar.com/SimplePDL/1.0/")]
    public partial class ctRectangle {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Transform;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double Opacity;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OpacitySpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Points;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Fill;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Stroke;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double StrokeWidth;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool StrokeWidthSpecified;
    }
}