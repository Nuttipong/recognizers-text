import delay from './delay';

const courses = [
  {
    id: "course-1",
    title: "Thor (Marvel Comics)",
    watchHref: "https://github.com/Nuttipong/react-redux-es6",
    authorId: "Thor",
    length: "6:20",
    category: "JavaScript"
  },
  {
    id: "course-2",
    title: "Hulk (film)",
    watchHref: "https://github.com/Nuttipong/react-redux-es6",
    authorId: "Hulk",
    length: "5:08",
    category: "ES6"
  },
  {
    id: "course-3",
    title: "Doctor Strange - Marvel",
    watchHref: "https://github.com/Nuttipong/react-redux-es6",
    authorId: "Dr-Strange",
    length: "3:10",
    category: "Webpack4"
  },
  {
    id: "course-4",
    title: "Black Widow - Marvel",
    watchHref: "https://github.com/Nuttipong/react-redux-es6",
    authorId: "Black Widow",
    length: "2:52",
    category: "Redux"
  },
  {
    id: "course-5",
    title: "Ultron - Marvel",
    watchHref: "https://github.com/Nuttipong/react-redux-es6",
    authorId: "Ultron",
    length: "2:30",
    category: "Career"
  },
  {
    id: "course-6",
    title: "Thanos",
    watchHref: "https://github.com/Nuttipong/react-redux-es6",
    authorId: "Thanos",
    length: "5:10",
    category: "React"
  }
];

function replaceAll(str, find, replace) {
  return str.replace(new RegExp(find, 'g'), replace);
}

const generateId = (course) => {
  return replaceAll(course.title, ' ', '-');
};

class CourseApi {
  static getAllCourses() {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve(Object.assign([], courses));
      }, delay);
    });
  }

  static saveCourse(course) {
    course = Object.assign({}, course);
    return new Promise((resolve, reject) => {
      setTimeout(() => {

        const minCourseTitleLength = 1;
        if (course.title.length < minCourseTitleLength) {
          reject(`Title must be at least ${minCourseTitleLength} characters.`);
        }

        if (course.id) {
          const existingCourseIndex = courses.findIndex(a => a.id == course.id);
          courses.splice(existingCourseIndex, 1, course);
        } else {
          course.id = generateId(course);
          course.watchHref = `https://github.com/Nuttipong/react-redux-es6/${course.id}`;
          courses.push(course);
        }

        resolve(course);
      }, delay);
    });
  }

  static deleteCourse(courseId) {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        const indexOfCourseToDelete = courses.findIndex(course => {
          return course.courseId == courseId;
        });
        courses.splice(indexOfCourseToDelete, 1);
        resolve();
      }, delay);
    });
  }
}

export default CourseApi;
