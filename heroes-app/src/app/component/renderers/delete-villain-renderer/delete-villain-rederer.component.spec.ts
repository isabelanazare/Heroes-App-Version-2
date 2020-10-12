import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DeleteVillainRendererComponent } from './delete-villain-renderer.component';

describe('DeleteVillainComponent', () => {
  let component: DeleteVillainRendererComponent;
  let fixture: ComponentFixture<DeleteVillainRendererComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeleteVillainRendererComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteVillainRendererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
