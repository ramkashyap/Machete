﻿namespace Machete.TranslateConfiguration
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using Configuration;
    using Translators;


    public class CopyInputValueTranslatePropertySpecification<TResult, TValue, TInput, TSchema> :
        TranslatePropertySpecification<TResult, Value<TValue>, TInput, TSchema>
        where TSchema : Entity
        where TInput : TSchema
        where TResult : TSchema
    {
        public CopyInputValueTranslatePropertySpecification(Expression<Func<TResult, Value<TValue>>> propertyExpression) :
            base(propertyExpression)
        {
        }

        public CopyInputValueTranslatePropertySpecification(PropertyInfo propertyInfo) :
            base(propertyInfo)
        {
        }

        public CopyInputValueTranslatePropertySpecification(Expression<Func<TResult, Value<TValue>>> propertyExpression,
            Expression<Func<TInput, Value<TValue>>> inputPropertyExpression)
            : base(propertyExpression, inputPropertyExpression)
        {
        }

        protected override IEnumerable<ValidateResult> Validate()
        {
            yield break;
        }

        public override void Apply(ITranslateBuilder<TResult, TInput, TSchema> builder)
        {
            var translator = new CopyValueEntityPropertyTranslator<TResult, TValue, TInput, TSchema>(builder.ImplementationType, ResultPropertyInfo, InputPropertyInfo);

            builder.Add(ResultPropertyInfo.Name, translator);
        }
    }
}