using UnityEngine;

using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

public static class PropertyFinderHelper
{
    private static Type[] RestrictedTypes = new Type[] { typeof(DataBinder) };

    public static IEnumerable<BindableMemberData> GetTargetProperties(GameObject gameObject)
    {
        var components = gameObject.GetComponents<Component>();
        var data = components.Where(c => c != null)
            .SelectMany(c =>
            {
                var type = c.GetType();
                return type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Select(propInfo => new BindableMemberData(type, propInfo));
            })
            .Where(memberDat => memberDat.property.GetSetMethod(false) != null
            && memberDat.property.GetGetMethod(false) != null
            && !RestrictedTypes.Contains(memberDat.type));

        return data.OrderBy(dat => dat.type.Name).ThenBy(dat => dat.property.PropertyType.Name);
    }

    public static IEnumerable<BindableMemberData> GetSourceProperties(GameObject gameObject)
    {
        var transform = gameObject.transform;
        var viewModels = new List<ViewModelBase>();

        while(transform != null)
        {
            var viewModel = transform.GetComponent<ViewModelBase>();
            if (viewModel == null || viewModel.transform == gameObject.transform)
            {
                transform = transform.parent;
                continue; 
            }

            viewModels.Add(viewModel);
            transform = transform.parent;
        }

        var data = viewModels.Where(vm => vm != null)
            .SelectMany(viewModel =>
            {
                var type = viewModel.GetType();
                return type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(t => t.GetCustomAttribute(typeof(BindableAttribute)) != null).Select(propInfo => new BindableMemberData(type, propInfo));
            })
            .Where(memberDat => memberDat.property.GetSetMethod(false) != null
            && memberDat.property.GetGetMethod(false) != null 
            && !RestrictedTypes.Contains(memberDat.type));

        return data.OrderBy(dat => dat.type.Name).ThenBy(dat => dat.property.PropertyType.Name);
    }
}

public class BindableMemberData
{
    public Type type;
    public PropertyInfo property;

    public string FullReference => property.DeclaringType + "." + property.Name;

    public BindableMemberData(Type type, PropertyInfo property)
    {
        this.type = type;
        this.property = property;
    }
}
