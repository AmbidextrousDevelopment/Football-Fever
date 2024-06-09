using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCoins : MonoBehaviour
{
        [SerializeField] private RectTransform coin; // Силка на RectTransform монети
        [SerializeField] private float minSpinSpeed = 200f; // Мінімальна швидкість обертання
        [SerializeField] private float maxSpinSpeed = 500f; // Максимальна швидкість обертання
        [SerializeField] private float minFallSpeed = 200f; // Мінімальна швидкість падіння
        [SerializeField] private float maxFallSpeed = 500f; // Максимальна швидкість падіння
        [SerializeField] private float resetPositionY = 310f; // Початкова позиція Y монети
        [SerializeField] private float lowerBoundY = -760f; // Значення Y нижче якого монета повернеться на початкову позицію

        private Vector2 initialPosition;
        private Coroutine spinCoroutine;

        private void Start()
        {
            initialPosition = coin.anchoredPosition;
        }

        private void OnEnable()
        {
            coin = GetComponent<RectTransform>();
            coin.anchoredPosition = new Vector2(coin.anchoredPosition.x, resetPositionY);
            spinCoroutine = StartCoroutine(SpinAndFall());
        }

    private void OnDisable()
    {
        if (spinCoroutine != null)
        {
            StopCoroutine(spinCoroutine);
            spinCoroutine = null;
        }
    }

    private IEnumerator SpinAndFall()
    {
        float spinSpeed = Random.Range(minSpinSpeed, maxSpinSpeed);
        float fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);

        while (true)
        {
            // Обертання монети
            coin.Rotate(0, 0, spinSpeed * Time.deltaTime);

            // Падіння монети по осі Y
            coin.anchoredPosition += new Vector2(0, -fallSpeed * Time.deltaTime);

            // Якщо монета опустилася нижче певного рівня, повертаємо її на початкову позицію по осі Y
            if (coin.anchoredPosition.y < lowerBoundY)
            {
                coin.anchoredPosition = new Vector2(coin.anchoredPosition.x, resetPositionY);
                // Встановлюємо нові випадкові швидкості обертання та падіння
                spinSpeed = Random.Range(minSpinSpeed, maxSpinSpeed);
                fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);
            }

            yield return null; // Чекаємо наступного кадру
        }
    }
}

