﻿namespace Machete.Translators.PropertyTranslaters
{
    using System;
    using System.Reflection;
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

        public CopyValueListPropertyTranslator(Type implementationType, PropertyInfo entityPropertyInfo, PropertyInfo inputPropertyInfo)
        {
            _property = new WriteProperty<TEntity, ValueList<TPropertyEntity>>(implementationType, entityPropertyInfo.Name);
            _inputProperty = new ReadOnlyProperty<TInput, ValueList<TPropertyEntity>>(inputPropertyInfo);
        }

        public Task Apply(TEntity entity, TranslateContext<TInput, TSchema> context)
        {
            var inputValue = _inputProperty.Get(context.Input) ?? ValueList.Missing<TPropertyEntity>();

            _property.Set(entity, inputValue);

            return TaskUtil.Completed;
        }
    }
}