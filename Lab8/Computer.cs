﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.8.3928.0.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/Computer.xsd")]
[System.Xml.Serialization.XmlRootAttribute("Computer", Namespace = "http://tempuri.org/Computer.xsd")]
public partial class Computer
{

    private string nameField;

    private string originField;

    private string priceField;

    private bool isCriticalField;

    private ComputerType[] typeField;

    /// <remarks/>
    public string Name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public string Origin
    {
        get
        {
            return this.originField;
        }
        set
        {
            this.originField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
    public string Price
    {
        get
        {
            return this.priceField;
        }
        set
        {
            this.priceField = value;
        }
    }

    /// <remarks/>
    public bool IsCritical
    {
        get
        {
            return this.isCriticalField;
        }
        set
        {
            this.isCriticalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Type")]
    public ComputerType[] Type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/Computer.xsd")]
public partial class ComputerType
{

    private bool isPeripheralField;

    private string energyConsumptionField;

    private bool hasCoolerField;

    private ComputerTypeTypeGroup typeGroupField;

    private ComputerTypePort portField;

    /// <remarks/>
    public bool IsPeripheral
    {
        get
        {
            return this.isPeripheralField;
        }
        set
        {
            this.isPeripheralField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
    public string EnergyConsumption
    {
        get
        {
            return this.energyConsumptionField;
        }
        set
        {
            this.energyConsumptionField = value;
        }
    }

    /// <remarks/>
    public bool HasCooler
    {
        get
        {
            return this.hasCoolerField;
        }
        set
        {
            this.hasCoolerField = value;
        }
    }

    /// <remarks/>
    public ComputerTypeTypeGroup TypeGroup
    {
        get
        {
            return this.typeGroupField;
        }
        set
        {
            this.typeGroupField = value;
        }
    }

    /// <remarks/>
    public ComputerTypePort Port
    {
        get
        {
            return this.portField;
        }
        set
        {
            this.portField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/Computer.xsd")]
public enum ComputerTypeTypeGroup
{

    /// <remarks/>
    Multimedia,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("In/Out")]
    InOut,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/Computer.xsd")]
public enum ComputerTypePort
{

    /// <remarks/>
    COM,

    /// <remarks/>
    USB,

    /// <remarks/>
    LPT,
}