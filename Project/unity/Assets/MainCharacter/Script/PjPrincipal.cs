using UnityEngine;
using System.Collections;

public class PjPrincipal {
	public static int MUERTO = 0;
	public static int VIVO = 1;
	public static int ATACANDO = 2;
	public static int INATACABLE = 3;
	public static int OTRO = 4; //por ejemplo cuando aun no ha empezado la partida
	
	private int estado; 
	private int poderAtaque; //el daño que causara al contrincante
	private int vida; //vida que posee el ser vivo
	private int maxVida;
	private int mana; //vida que posee el ser vivo
	private int maxMana;
	public PjPrincipal pj;
	
	/**************************************************************
	 *                         Getters del jugador
     **************************************************************
	 */
	/*
	public int getEstado();
	public int getVida();
	public int getPoderAtaque();
*/
	/**************************************************************
	 *                         Setters del jugador
     **************************************************************
	 */
	
	
	/*  Espera estados del 0 al 3. Estan descritos en la definicion del atributo
		devuelve el Estado cambiado
		en caso que devuelva -1, no se cambio el estado correctamente
	*/
	/*
	public int setEstado(int estado);
	public void setVida(int vida);
	public void setPoderAtaque(int poderAtaque);
	*/
	/*
	 *  Metodos de interes relacionados con jugador
	 */
	/*
	 * Devuelve el estado del personaje. Util en casos como
	 * que el personaje muere o es inatacable.
	 */
	//public int restarVida(int vida);
	
	public PjPrincipal(int estado, int poderAtaque, int vida){
		this.setEstado (OTRO);
		this.setEstado (estado);
		this.setPoderAtaque (poderAtaque);
		this.setVida (vida);
		this.setMaxVida (vida);
		this.vida = 80;
		this.maxVida = 100;
		this.mana = 70;
		this.maxMana = 100;
		this.pj = this;
	}	
	
	public int getEstado(){
		return this.estado;
	}
	public int getVida(){
		Debug.Log ("vida=" + vida);
		return this.vida;
	}
	public int getMana(){
		return this.mana;
	}
	public int getPoderAtaque(){
		return this.poderAtaque;
	}
	public int getMaxVida(){
		return this.maxVida; 
	}
	public int getMaxMana(){
		return this.maxMana; 
	}
	public int setEstado(int estado){
		//Faltaria revisar si viene del estado inatacable ¿puede ir a muerto?
		if (estado >= 0 && estado <= 3)
			this.estado = estado;
		else
			return -1;
		return this.estado;
	}
	public void setVida(int vida){
		this.vida = vida;
	}
	public int restarVida(int vida){
		if (this.getEstado () == VIVO || 
		    this.getEstado () == ATACANDO) {
			
			int vidaTemp = this.getVida () - vida;
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			
			if (vidaTemp <= 0) {
				this.setEstado (MUERTO);
				/*
				 * Cridar escena de GameOver i esborrar tot lo que calgui del joc 
				 * 
				 */
				
			}else{
				this.setVida(vidaTemp);
			}
		}
		
		return this.getEstado ();
	}
	public void setPoderAtaque(int poderAtaque){
		this.poderAtaque = poderAtaque;
	}
	public void setMaxVida(int maxVida){
		this.maxVida = maxVida;
	}
	public int getSelectedSpell(){
		return 1;//sha de canviar
	}
	
}