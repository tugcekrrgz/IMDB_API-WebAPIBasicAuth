$(document).ready(function(){

    const URL="https://localhost:7086/api/movie";

    function GetMovies(){
        //Ajax
        $.ajax({
            url:URL,
            type:'Get',
            success:function(data){
                MoviesCard(data);
                console.log(data);                
            },
            error:function(p1,p2,p3){
                Swal.fire(
                    'Hata!',
                    'Servera ulaşılamıyor!',
                    'error'
                )
            }
        })
    }

    //Card Function
    function MoviesCard(movies){
        $("#movieRow").empty();
        movies.forEach(function(val,index){
            $("#movieRow").append(`
            <div class="col-md-3">
                <div class="card">
                    <img
                        src="images/placeholder.png"
                        class="card-img-top"
                        alt="..."
                    />
                    <div class="card-body">
                        <h5 class="card-title">${val.title}</h5>
                        <div class="d-flex justify-content-between">
                            <a href="#" class="nav-link">${val.year}</a>
                            <a href="#" class="nav-link">${val.rating}</a>
                        </div>
                    </div>
                </div>
            </div>
            `)
        })
    }

    /*
    keydown --> tuş aşağı indiği anda
    keypress --> tuş indi çıktı
    keyup --> tuş inip çıktıktan sonra
    */
    //Search Function
    $("#search").on("keyup",function(e){      
        const value=e.target.value;

        //Ajax
        $.ajax({
            url:`${URL}/${value}`,
            type:'Get',
            success:function(data){
                MoviesCard(data);
                console.log(data);                
            },
            error:function(p1,p2,p3){
                Swal.fire(
                    'Hata!',
                    'Servera ulaşılamıyor!',
                    'error'
                )
            }
        })

       
    })

    //Random Button event
    $("#randomMovie").on("click",function(){
        GetRandomMovie();
    })

    //FilterFunction
    //RandomMovie
    function GetRandomMovie(){
        $.ajax({
            url:`${URL}/randommovie`,
            type:'Get',
            success:function(data){
                RandomMovieCard(data);
                console.log(data);
            },
            error:function(p1,p2,p3){
                Swal.fire(
                    'Hata!',
                    'Servera ulaşılamıyor!',
                    'error'
                )
            }
        })
    }

    function RandomMovieCard(movie){
        $("#movieRow").empty();
        $("#movieRow").append(`
            <div class="col-md-6 text-center">
                <div class="card">
                    <img
                        src="images/placeholder.png"
                        class="card-img-top"
                        alt="..."
                    />
                    <div class="card-body">
                        <h5 class="card-title">${movie.title}</h5>
                        <div class="d-flex justify-content-between">
                            <a href="#" class="nav-link">${movie.year}</a>
                            <a href="#" class="nav-link">${movie.rating}</a>
                        </div>
                    </div>
                </div>
            </div>
        `)
    }


    GetMovies();
})




// const movie=() => {
//     return "test";
// }



