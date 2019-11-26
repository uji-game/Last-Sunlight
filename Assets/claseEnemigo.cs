using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaseEnemigo {
    private float HP;
    private float Dmg;
    private float posX;
    private float posY;
    public ClaseEnemigo(float HP, float Dmg, float posX, float posY) 
    {

        this.HP = HP;
        this.Dmg = Dmg;
        this.posX = posX;
        this.posY = posY;

    }
    public float GetX() { return posX; }
    public float GetY() { return posY; }

    public void Atacar() {  }
    public void Patrulla() {  }
    

}


