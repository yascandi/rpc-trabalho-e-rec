using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable // interface que marca as classes que podem levar dano
{
    void TakeDamage(); // m�todo que deve ser chamado quando a classe levar dano
}

