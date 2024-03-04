﻿using System.Reflection;
using CustomerService.Core.Entities;
using Eshop.Core.Domain.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CustomerService.Infrastructure.Data.CompiledModels;

public partial class CreditCardEntityType
{
    public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType? baseEntityType = null)
    {
        var runtimeEntityType = model.AddEntityType(
            "CustomerService.AppCore.Core.Entities.CreditCard",
            typeof(CreditCard),
            baseEntityType);

        var id = runtimeEntityType.AddProperty(
            "Id",
            typeof(Guid),
            propertyInfo: typeof(EntityBase).GetProperty("Id",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(EntityBase).GetField("<Id>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            valueGenerated: ValueGenerated.OnAdd,
            afterSaveBehavior: PropertySaveBehavior.Throw);
        id.AddAnnotation("Relational:ColumnName", "id");
        id.AddAnnotation("Relational:ColumnType", "uuid");
        id.AddAnnotation("Relational:DefaultValueSql", "uuid_generate_v4()");

        var active = runtimeEntityType.AddProperty(
            "Active",
            typeof(bool),
            propertyInfo: typeof(CreditCard).GetProperty("Active",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(CreditCard).GetField("<Active>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
        active.AddAnnotation("Relational:ColumnName", "active");

        var cardNumber = runtimeEntityType.AddProperty(
            "CardNumber",
            typeof(string),
            propertyInfo: typeof(CreditCard).GetProperty("CardNumber",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(CreditCard).GetField("<CardNumber>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            maxLength: 16);
        cardNumber.AddAnnotation("Relational:ColumnName", "card_number");

        var created = runtimeEntityType.AddProperty(
            "Created",
            typeof(DateTime),
            propertyInfo: typeof(EntityBase).GetProperty("Created",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(EntityBase).GetField("<Created>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            valueGenerated: ValueGenerated.OnAdd);
        created.AddAnnotation("Relational:ColumnName", "created");
        created.AddAnnotation("Relational:DefaultValueSql", "now()");

        var customerId = runtimeEntityType.AddProperty(
            "CustomerId",
            typeof(Guid));
        customerId.AddAnnotation("Relational:ColumnName", "customer_id");

        var expiry = runtimeEntityType.AddProperty(
            "Expiry",
            typeof(DateTime),
            propertyInfo: typeof(CreditCard).GetProperty("Expiry",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(CreditCard).GetField("<Expiry>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
        expiry.AddAnnotation("Relational:ColumnName", "expiry");

        var nameOnCard = runtimeEntityType.AddProperty(
            "NameOnCard",
            typeof(string),
            propertyInfo: typeof(CreditCard).GetProperty("NameOnCard",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(CreditCard).GetField("<NameOnCard>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            maxLength: 20);
        nameOnCard.AddAnnotation("Relational:ColumnName", "name_on_card");

        var updated = runtimeEntityType.AddProperty(
            "Updated",
            typeof(DateTime?),
            propertyInfo: typeof(EntityBase).GetProperty("Updated",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(EntityBase).GetField("<Updated>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            nullable: true);
        updated.AddAnnotation("Relational:ColumnName", "updated");

        var key = runtimeEntityType.AddKey(
            new[] { id });
        runtimeEntityType.SetPrimaryKey(key);
        key.AddAnnotation("Relational:Name", "pk_credit_cards");

        var index = runtimeEntityType.AddIndex(
            new[] { customerId });
        index.AddAnnotation("Relational:Name", "ix_credit_cards_customer_id");

        var index0 = runtimeEntityType.AddIndex(
            new[] { id },
            unique: true);
        index0.AddAnnotation("Relational:Name", "ix_credit_cards_id");

        return runtimeEntityType;
    }

    public static RuntimeForeignKey CreateForeignKey1(RuntimeEntityType declaringEntityType,
        RuntimeEntityType principalEntityType)
    {
        var runtimeForeignKey = declaringEntityType.AddForeignKey(
            new[] { declaringEntityType.FindProperty("CustomerId")! },
            principalEntityType.FindKey(new[] { principalEntityType.FindProperty("Id")! })!,
            principalEntityType,
            deleteBehavior: DeleteBehavior.Cascade,
            required: true);

        var customer = declaringEntityType.AddNavigation("Customer",
            runtimeForeignKey,
            onDependent: true,
            typeof(Customer),
            propertyInfo: typeof(CreditCard).GetProperty("Customer",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(CreditCard).GetField("<Customer>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

        var creditCards = principalEntityType.AddNavigation("CreditCards",
            runtimeForeignKey,
            onDependent: false,
            typeof(IEnumerable<CreditCard>),
            propertyInfo: typeof(Customer).GetProperty("CreditCards",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Customer).GetField("_creditCards",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

        runtimeForeignKey.AddAnnotation("Relational:Name", "fk_credit_cards_customers_customer_id");
        return runtimeForeignKey;
    }

    public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
    {
        runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
        runtimeEntityType.AddAnnotation("Relational:Schema", "customer");
        runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
        runtimeEntityType.AddAnnotation("Relational:TableName", "credit_cards");
        runtimeEntityType.AddAnnotation("Relational:ViewName", null);
        runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

        Customize(runtimeEntityType);
    }

    static partial void Customize(RuntimeEntityType runtimeEntityType);
}