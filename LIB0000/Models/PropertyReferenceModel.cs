using System;
using System.Reflection;

namespace LIB0000
{
    public class PropertyReferenceModel
    {


        #region Commands
        #endregion

        #region Constructor
        public PropertyReferenceModel(object targetObject, string propertyName)
        {
            TargetObject = targetObject;
            PropertyName = propertyName;
        }
        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods

        public void SetValue(object newValue)
        {
            var property = TargetObject.GetType().GetProperty(PropertyName);
            if (property != null && property.CanWrite)
            {
                property.SetValue(TargetObject, newValue);
            }
        }

        public object GetValue()
        {
            var property = TargetObject.GetType().GetProperty(PropertyName);
            return property?.GetValue(TargetObject);
        }
        #endregion

        #region Properties        
        public object TargetObject { get; }
        public string PropertyName { get; }
        #endregion
    }
}