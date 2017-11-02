﻿namespace Machete.Translators.PropertyTranslaters
{
    using System;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Internals.Extensions;
    using Internals.Reflection;


    public class CopyValueListPropertyTranslator<TEntity, TPropertyEntity, TInput, TSchema> :
        IPropertyTranslator<TEntity, TInput, TSchema>
        where TSchema : Entity
        where TInput : TSchema
        where TEntity : TSchema
    {
        readonly WriteProperty<TEntity, ValueList<TPropertyEntity>> _property;
        readonly ReadOnlyProperty<TInput, ValueList<TPropertyEntity>> _inputProperty;
        readonly string _propertyName;

        public CopyValueListPropertyTranslator(Type implementationType, PropertyInfo entityPropertyInfo, PropertyInfo inputPropertyInfo)
        {
            _propertyName = entityPropertyInfo.Name;
            _property = new WriteProperty<TEntity, ValueList<TPropertyEntity>>(implementationType, _propertyName);
            _inputProperty = new ReadOnlyProperty<TInput, ValueList<TPropertyEntity>>(inputPropertyInfo);
        }

        public Task Apply(TEntity entity, TranslateContext<TInput, TSchema> context)
        {
            var inputValue = _inputProperty.Get(context.Input) ?? ValueList.Missing<TPropertyEntity>();

            _property.Set(entity, inputValue);

            return TaskUtil.Completed;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(_propertyName);
            sb.Append(": (copy");

            if (_propertyName != _inputProperty.Property.Name)
                sb.AppendFormat(", source: {0}", _inputProperty.Property.Name);

            sb.AppendLine(")");

            return sb.ToString();
        }
    }
}