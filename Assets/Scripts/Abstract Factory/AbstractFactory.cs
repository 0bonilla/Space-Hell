using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFactory
{
    public AbstractFactory(IProduct productToProduce)
    {
        product = productToProduce;
    }

    protected IProduct product;
    public abstract IProduct CreateProduct();
}

public interface IProduct
{
    IProduct Clone();
    GameObject MyGameObject { get; }
}
