function solve(fruit, weight, price){
    console.log(`I need ${(price * weight / 1000).toFixed(2)} leva to buy ${(weight / 1000).toFixed(2)} kilograms ${fruit}.`);
}
solve('orange', 2500, 1.80);