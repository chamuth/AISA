
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class GoodreadsResponse
{

    private GoodreadsResponseRequest requestField;

    private GoodreadsResponseSearch searchField;

    /// <remarks/>
    public GoodreadsResponseRequest Request
    {
        get
        {
            return this.requestField;
        }
        set
        {
            this.requestField = value;
        }
    }

    /// <remarks/>
    public GoodreadsResponseSearch search
    {
        get
        {
            return this.searchField;
        }
        set
        {
            this.searchField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GoodreadsResponseRequest
{

    private bool authenticationField;

    private string keyField;

    private string methodField;

    /// <remarks/>
    public bool authentication
    {
        get
        {
            return this.authenticationField;
        }
        set
        {
            this.authenticationField = value;
        }
    }

    /// <remarks/>
    public string key
    {
        get
        {
            return this.keyField;
        }
        set
        {
            this.keyField = value;
        }
    }

    /// <remarks/>
    public string method
    {
        get
        {
            return this.methodField;
        }
        set
        {
            this.methodField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GoodreadsResponseSearch
{

    private string queryField;

    private byte resultsstartField;

    private byte resultsendField;

    private uint totalresultsField;

    private string sourceField;

    private decimal querytimesecondsField;

    private GoodreadsResponseSearchWork[] resultsField;

    /// <remarks/>
    public string query
    {
        get
        {
            return this.queryField;
        }
        set
        {
            this.queryField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("results-start")]
    public byte resultsstart
    {
        get
        {
            return this.resultsstartField;
        }
        set
        {
            this.resultsstartField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("results-end")]
    public byte resultsend
    {
        get
        {
            return this.resultsendField;
        }
        set
        {
            this.resultsendField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("total-results")]
    public uint totalresults
    {
        get
        {
            return this.totalresultsField;
        }
        set
        {
            this.totalresultsField = value;
        }
    }

    /// <remarks/>
    public string source
    {
        get
        {
            return this.sourceField;
        }
        set
        {
            this.sourceField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("query-time-seconds")]
    public decimal querytimeseconds
    {
        get
        {
            return this.querytimesecondsField;
        }
        set
        {
            this.querytimesecondsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("work", IsNullable = false)]
    public GoodreadsResponseSearchWork[] results
    {
        get
        {
            return this.resultsField;
        }
        set
        {
            this.resultsField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GoodreadsResponseSearchWork
{

    private GoodreadsResponseSearchWorkID idField;

    private GoodreadsResponseSearchWorkBooks_count books_countField;

    private GoodreadsResponseSearchWorkRatings_count ratings_countField;

    private GoodreadsResponseSearchWorkText_reviews_count text_reviews_countField;

    private GoodreadsResponseSearchWorkOriginal_publication_year original_publication_yearField;

    private GoodreadsResponseSearchWorkOriginal_publication_month original_publication_monthField;

    private GoodreadsResponseSearchWorkOriginal_publication_day original_publication_dayField;

    private decimal average_ratingField;

    private GoodreadsResponseSearchWorkBest_book best_bookField;

    /// <remarks/>
    public GoodreadsResponseSearchWorkID id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public GoodreadsResponseSearchWorkBooks_count books_count
    {
        get
        {
            return this.books_countField;
        }
        set
        {
            this.books_countField = value;
        }
    }

    /// <remarks/>
    public GoodreadsResponseSearchWorkRatings_count ratings_count
    {
        get
        {
            return this.ratings_countField;
        }
        set
        {
            this.ratings_countField = value;
        }
    }

    /// <remarks/>
    public GoodreadsResponseSearchWorkText_reviews_count text_reviews_count
    {
        get
        {
            return this.text_reviews_countField;
        }
        set
        {
            this.text_reviews_countField = value;
        }
    }

    /// <remarks/>
    public GoodreadsResponseSearchWorkOriginal_publication_year original_publication_year
    {
        get
        {
            return this.original_publication_yearField;
        }
        set
        {
            this.original_publication_yearField = value;
        }
    }

    /// <remarks/>
    public GoodreadsResponseSearchWorkOriginal_publication_month original_publication_month
    {
        get
        {
            return this.original_publication_monthField;
        }
        set
        {
            this.original_publication_monthField = value;
        }
    }

    /// <remarks/>
    public GoodreadsResponseSearchWorkOriginal_publication_day original_publication_day
    {
        get
        {
            return this.original_publication_dayField;
        }
        set
        {
            this.original_publication_dayField = value;
        }
    }

    /// <remarks/>
    public decimal average_rating
    {
        get
        {
            return this.average_ratingField;
        }
        set
        {
            this.average_ratingField = value;
        }
    }

    /// <remarks/>
    public GoodreadsResponseSearchWorkBest_book best_book
    {
        get
        {
            return this.best_bookField;
        }
        set
        {
            this.best_bookField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GoodreadsResponseSearchWorkID
{

    private string typeField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
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

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public uint Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GoodreadsResponseSearchWorkBooks_count
{

    private string typeField;

    private ushort valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
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

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public ushort Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GoodreadsResponseSearchWorkRatings_count
{

    private string typeField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
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

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public uint Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GoodreadsResponseSearchWorkText_reviews_count
{

    private string typeField;

    private ushort valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
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

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public ushort Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GoodreadsResponseSearchWorkOriginal_publication_year
{

    private string typeField;

    private ushort valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
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

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public ushort Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GoodreadsResponseSearchWorkOriginal_publication_month
{

    private string typeField;

    private bool nilField;

    private bool nilFieldSpecified;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
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

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool nil
    {
        get
        {
            return this.nilField;
        }
        set
        {
            this.nilField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool nilSpecified
    {
        get
        {
            return this.nilFieldSpecified;
        }
        set
        {
            this.nilFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GoodreadsResponseSearchWorkOriginal_publication_day
{

    private string typeField;

    private bool nilField;

    private bool nilFieldSpecified;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
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

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool nil
    {
        get
        {
            return this.nilField;
        }
        set
        {
            this.nilField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool nilSpecified
    {
        get
        {
            return this.nilFieldSpecified;
        }
        set
        {
            this.nilFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GoodreadsResponseSearchWorkBest_book
{

    private GoodreadsResponseSearchWorkBest_bookID idField;

    private string titleField;

    private GoodreadsResponseSearchWorkBest_bookAuthor authorField;

    private string image_urlField;

    private string small_image_urlField;

    private string typeField;

    /// <remarks/>
    public GoodreadsResponseSearchWorkBest_bookID id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public string title
    {
        get
        {
            return this.titleField;
        }
        set
        {
            this.titleField = value;
        }
    }

    /// <remarks/>
    public GoodreadsResponseSearchWorkBest_bookAuthor author
    {
        get
        {
            return this.authorField;
        }
        set
        {
            this.authorField = value;
        }
    }

    /// <remarks/>
    public string image_url
    {
        get
        {
            return this.image_urlField;
        }
        set
        {
            this.image_urlField = value;
        }
    }

    /// <remarks/>
    public string small_image_url
    {
        get
        {
            return this.small_image_urlField;
        }
        set
        {
            this.small_image_urlField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GoodreadsResponseSearchWorkBest_bookID
{

    private string typeField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
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

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public uint Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GoodreadsResponseSearchWorkBest_bookAuthor
{

    private GoodreadsResponseSearchWorkBest_bookAuthorID idField;

    private string nameField;

    /// <remarks/>
    public GoodreadsResponseSearchWorkBest_bookAuthorID id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public string name
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
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GoodreadsResponseSearchWorkBest_bookAuthorID
{

    private string typeField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
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

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public uint Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

