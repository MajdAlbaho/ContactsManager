using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;

namespace ContactsManager.Api.Helper
{
    public static class ExpandObject
    {
        public static object CreateDataShapedObject<TModel>(TModel model, List<string> listOfFields) {

            if (!listOfFields.Any()) {
                return model;
            }
            else {
                // create a new ExpandoObject & dynamically create the properties for this object
                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in listOfFields) {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = model.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(model, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<string, object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }
    }
}
