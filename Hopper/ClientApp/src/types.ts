/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.11.1.0 (NJsonSchema v10.4.3.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming



export interface LoginDto {
    email: string;
    password: string;
}

export interface RegisterDto extends LoginDto {
    firstName: string;
    lastName: string;
    language: string;
}

export interface SoundEditDto {
    id?: string | undefined;
    title: string;
    author: string;
    video: string;
    language: string;
}

export interface SoundDto extends SoundEditDto {
    likes: number;
    liked: boolean;
    address: string;
    creator: boolean;
}

export enum SoundFilter {
    Liked = 0,
    Created = 1,
}

export enum SoundSortBy {
    Title = 0,
    Author = 1,
    Likes = 2,
}

export interface UserDto {
    id: string;
    firstName: string;
    lastName: string;
    language: string;
    createdSounds: SoundEditDto[];
    likedSounds: SoundEditDto[];
}

export interface FileParameter {
    data: any;
    fileName: string;
}

export interface FileResponse {
    data: Blob;
    status: number;
    fileName?: string;
    headers?: { [name: string]: any };
}