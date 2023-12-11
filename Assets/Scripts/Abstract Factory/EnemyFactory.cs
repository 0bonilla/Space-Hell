using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : AbstractFactory
{
    public EnemyFactory(IProduct productToProduce) : base(productToProduce)
    {

    }

    public override IProduct CreateProduct()
    {
        return product.Clone();
    }
}

