import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { Member } from 'src/app/Models/Member';
import { MembersService } from 'src/app/_Services/members.service';

@Component({
  selector: 'app-members-details',
  templateUrl: './members-details.component.html',
  styleUrls: ['./members-details.component.css']
})
export class MembersDetailsComponent implements OnInit {
  member:Member | undefined
  galleryOptions : NgxGalleryOptions[] = [];
  galleryImages : NgxGalleryImage[] = [];
  constructor( private memberservice:MembersService, private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMember()

    this.galleryOptions=[
      {
        width : '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false

      }
    ]
  // this.galeryImages=this.getImages();
    
  }
  getImages(){
    if(!this.member) return ['https://randomuser.me/api/portraits/women/54.jpg'];
    const imageurls=[];
    for (const photo of this.member.photos){
      imageurls.push({
        small:photo.url,
        medium:photo.url,
        big:photo.url 

      })
    }
    return imageurls;
  }
  loadMember(){
    const username=this.route.snapshot.paramMap.get("username");
    if(!username) return;
    this.memberservice.getMember(username).subscribe({
      next : member => {
        this.member = member;
        this.galleryImages=this.getImages();
      }
    })
  }
}
