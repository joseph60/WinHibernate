using System;

namespace WinHibernate.Domain
{
    #region Product

    /// <summary>
    /// Product object for NHibernate mapped table 'Product'.
    /// </summary>
    public class Product
	{
		#region Member Variables
		
		protected Guid _id;
		protected string _name;
		protected string _category;
		protected bool _discontinued;

		#endregion

		#region Constructors

		public Product() { }

		public Product( string name, string category, bool discontinued )
		{
			this._name = name;
			this._category = category;
			this._discontinued = discontinued;
		}

		#endregion

		#region Public Properties

		public virtual Guid Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 255)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

		public virtual string Category
		{
			get { return _category; }
			set
			{
				if ( value != null && value.Length > 255)
					throw new ArgumentOutOfRangeException("Invalid value for Category", value, value.ToString());
				_category = value;
			}
		}

		public virtual bool Discontinued
		{
			get { return _discontinued; }
			set { _discontinued = value; }
		}

		

		#endregion
	}
	#endregion
}