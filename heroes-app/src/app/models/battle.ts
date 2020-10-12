import { Hero } from './hero';
import { Villain } from './villain';

export class Battle {
  public id: number;
  public initiatorId: number;
  public opponentId: number;
  public latitude: number;
  public longitude: number;
  public heroes: Hero[];
  public villains: Villain[];
  public villainsStrength: number;
  public heroesStrength: number;

  constructor(id?: number) {
    this.id = id;
  }
}
