import { BadgesCount } from './../../models/badges-count';
import { BattleService } from './../../service/battle.service';
import { Component, Input, OnInit } from '@angular/core';
import { Hero } from 'src/app/models/hero';

@Component({
  selector: 'app-hero-badges',
  templateUrl: './hero-badges.component.html',
  styleUrls: ['./hero-badges.component.css'],
})
export class HeroBadgesComponent implements OnInit {
  @Input() hero: Hero;
  public tierBadges: { img: string; count: number }[] = [];
  public tier1Badges: number;
  public tier2Badges: number;
  public tier3Badges: number;
  public tier4Badges: number;
  public tier5Badges: number;
  public heroTier: number;

  constructor(private battleService: BattleService) {}

  ngOnInit(): void {
    this.battleService
      .getBadgesCount(this.hero.id)
      .subscribe((badgesCount: BadgesCount) => {
        this.tierBadges.push({
          img: '../../../assets/image/tier1.svg',
          count: badgesCount.tier1,
        });
        this.tierBadges.push({
          img: '../../../assets/image/tier2.svg',
          count: badgesCount.tier2,
        });
        this.tierBadges.push({
          img: '../../../assets/image/tier3.svg',
          count: badgesCount.tier3,
        });
        this.tierBadges.push({
          img: '../../../assets/image/tier4.svg',
          count: badgesCount.tier4,
        });
        this.tierBadges.push({
          img: '../../../assets/image/tier5.svg',
          count: badgesCount.tier5,
        });
        let tier = 1;
        for (let i: number = 1; i < 5; i++) {
          if (this.tierBadges[i].count > 0) {
            tier = i + 1;
          }
          this.heroTier = tier;
        }
      });
  }
}
